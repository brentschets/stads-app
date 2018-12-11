using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels.Account
{
    public sealed class RegisterEntrepreneurViewModel: INotifyPropertyChanged
    {
        public ICommand FileUpload => new RelayCommand(o => UploadFile());

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

        private List<Category> _categories;

        public List<Category> Categories {
            get => _categories;
            private set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public async void UploadFile()
        {
            var folderPicker = new Windows.Storage.Pickers.FileOpenPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add(".jpg");
            folderPicker.FileTypeFilter.Add(".jpeg");
            folderPicker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile folder = await folderPicker.PickSingleFileAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                    FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                Debug.WriteLine("file selected");

            }
        }

        public RegisterEntrepreneurViewModel()
        {

        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Category>("Categories");
        }

        public async Task LoadData()
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
