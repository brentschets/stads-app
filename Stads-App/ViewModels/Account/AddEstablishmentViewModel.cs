using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Account
{
    public class AddEstablishmentViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        public Frame Frame { private get; set; }

        public int StoreId { private get; set; }

        private string _errorMsg;

        public string ErrorMsg
        {
            get => _errorMsg;
            private set
            {
                _errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }

        private bool _isActive = true;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public string Street { get; set; }

        public string Number { get; set; }

        private string _pickerButtonText = "Kies een afbeelding";

        public string PickerButtonText
        {
            get => _pickerButtonText;
            private set
            {
                _pickerButtonText = value;
                OnPropertyChanged(nameof(PickerButtonText));
            }
        }

        private StorageFile Image { get; set; }

        public ICommand SelectFileCommand => new RelayCommand(o => SelectFile());

        private async void SelectFile()
        {
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop
            };
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");

            var file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(
                    "PickedFolderToken", file);
                PickerButtonText = file.Name;
                Image = file;
            }
            else
            {
                PickerButtonText = "Kies een afbeelding";
                Image = null;
            }
        }

        public ICommand AddEStablishmentCommand => new RelayCommand(o => AddEstablishment());

        private async void AddEstablishment()
        {
            IsActive = false;

            if (Image == null || string.IsNullOrWhiteSpace(Street) || string.IsNullOrWhiteSpace(Number))
            {
                ErrorMsg = "Alle velden moeten ingevuld worden";
                IsActive = true;
                return;
            }

            var address = new Address
            {
                Street = Street,
                Number = Number
            };
            var establishment = new Establishment
            {
                Address = address,
                Store = new Store {StoreId = StoreId},
                Visited = 0
            };

            await _userManager.AddEstablishmentAsync(establishment, Image);

            Frame.GoBack();
        }

        public AddEstablishmentViewModel()
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