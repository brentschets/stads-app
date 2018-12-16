using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels.Account
{
    public sealed class RegisterEntrepreneurViewModel : INotifyPropertyChanged
    {
        public ICommand FileUpload => new RelayCommand(o => UploadFile());

        public ICommand RegisterEntrepreneurCommand => new RelayCommand(o => RegisterEntrepreneur());

        public ICommand CategoryChangedCommand => new RelayCommand(CategoryChanged);

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

        public string ErrorMsg
        {
            get => _errorMsg;
            private set
            {
                _errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Description { get; set; }

        public string NameEntrepreneur { get; set; }

        private StorageFile Image { get; set; }

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

        private List<Category> _categories;

        public List<Category> Categories
        {
            get => _categories;
            private set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        private Category SelectedCategory { get; set; }

        private async void UploadFile()
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
        }

        private void CategoryChanged(object o)
        {
            if (o is SelectionChangedEventArgs args) SelectedCategory = args.AddedItems[0] as Category;
        }

        private async void RegisterEntrepreneur()
        {
            IsActive = false;

            var store = new Store
            {
                Name = NameEntrepreneur,
                Description = Description,
                Category = SelectedCategory
            };

            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Password = Password
            };

            var result = await StadsAppRestApiClient.Instance.RegisterStoreAsync(store, Image, user);

            if (result.Success) ErrorMsg = "Geregistreerd";
            else
            {
                ErrorMsg = result.Error?.Message;
                IsActive = true;
            }
        }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Category>("Categories");
        }

        public async Task LoadDataAsync()
        {
            Categories = await GetCategoriesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}