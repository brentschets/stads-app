using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Account
{
    public sealed class EditEstablishmentViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Event> _events;

        public ObservableCollection<Event> Events
        {
            get => _events;
            private set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        public ICommand RemoveEventCommand => new RelayCommand(RemoveEvent);

        private async void RemoveEvent(object args)
        {
            var eventId = (int) args;
            await StadsAppRestApiClient.Instance.DeleteEventAsync(eventId);
            var @event = Events.Single(e => e.EventId == eventId);
            Events.Remove(@event);
        }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public ICommand AddEventCommand => new RelayCommand(o => AddEvent());

        private async void AddEvent()
        {
            var @event = new Event
            {
                Name = EventName,
                Description = EventDescription,
                Establishment = Establishment
            };

            await _userManager.AddEventAsync(@event);
            Events.Add(@event);
        }

        public EditEstablishmentViewModel()
        {
            _userManager = new UserManager();
        }

        public async void LoadDataAsync()
        {
            Events = new ObservableCollection<Event>(
                await StadsAppRestApiClient.Instance.GetListAsync<Event>(
                    $"Events/ForEstablishment/{Establishment.EstablishmentId}"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}