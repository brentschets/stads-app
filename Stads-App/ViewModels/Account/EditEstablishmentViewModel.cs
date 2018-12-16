using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Account
{
    public class EditEstablishmentViewModel : INotifyPropertyChanged
    {
        public Establishment Establishment
        {
            private get { return _establishment; }
            set
            {
                _establishment = value;
                Street = value.Address.Street;
                Number = value.Address.Number;
            }
        }

        public Frame Frame { private get; set; }
        private readonly UserManager _userManager;

        private bool _isActive = true;

        public bool IsActive
        {
            get => _isActive;
            private set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        private string _errorMsg;
        private Establishment _establishment;

        public string ErrorMsg
        {
            get => _errorMsg;
            private set
            {
                _errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }

        private string _street;

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        private string _number;

        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public ICommand EditEstablishmentCommand => new RelayCommand(o => EditEstablishment());

        private async void EditEstablishment()
        {
            IsActive = false;

            if (string.IsNullOrWhiteSpace(Street) || string.IsNullOrWhiteSpace(Number))
            {
                ErrorMsg = "Alle velden moeten ingevuld worden";
                IsActive = true;
                return;
            }

            var establishment = new Establishment
            {
                EstablishmentId = Establishment.EstablishmentId,
                Address = new Address
                {
                    Street = Street,
                    Number = Number
                },
                Store = Establishment.Store
            };

            if (establishment.EqualsForUpdate(Establishment))
            {
                ErrorMsg = "Geen veranderingen";
                IsActive = true;
                return;
            }

            await _userManager.UpdateEstablishmentAsync(establishment);

            Frame.GoBack();
        }

        public EditEstablishmentViewModel()
        {
            _userManager = new UserManager();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}