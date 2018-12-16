using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;
using Stads_App.Views.Account;

namespace Stads_App.ViewModels.Account
{
    public sealed class EntrepreneurViewModel : INotifyPropertyChanged
    {
        public Frame Frame { private get; set; }
        private readonly UserManager _userManager;
        private Store Store { get; set; }

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

        private string _storeName;

        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        private string _storeDescription;

        public string StoreDescription
        {
            get => _storeDescription;
            set
            {
                _storeDescription = value;
                OnPropertyChanged(nameof(StoreDescription));
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

        public ICommand CategoryChangedCommand => new RelayCommand(CategoryChanged);

        private void CategoryChanged(object o)
        {
            if (o is SelectionChangedEventArgs args) SelectedCategory = args.AddedItems[0] as Category;
        }

        public ICommand UpdateStoreCommand => new RelayCommand(o => UpdateStore());

        private async void UpdateStore()
        {
            var store = new Store
            {
                StoreId = Store.StoreId,
                Name = StoreName,
                Description = StoreDescription,
                Category = SelectedCategory
            };
            if (store.EqualsForUpdate(Store))
            {
                ErrorMsg = "Geen veranderingen";
                return;
            }

            var response = await _userManager.UpdateStoreAsync(store);
            if (response.Success)
            {
                ErrorMsg = string.Empty;
                Store = await GetStoreAsync();
            }
            else
            {
                ErrorMsg = response.Error?.Message;
            }
        }

        private ObservableCollection<Establishment> _establishments;

        public ObservableCollection<Establishment> Establishments
        {
            get => _establishments;
            private set
            {
                _establishments = value;
                OnPropertyChanged(nameof(Establishments));
            }
        }

        public ICommand UpdateEstablishmentCommand => new RelayCommand(UpdateEstablishment);

        private void UpdateEstablishment(object args)
        {
            Frame.Navigate(typeof(EditEstablishment), args as Establishment);
        }

        public ICommand DeleteEstablishmentCommand => new RelayCommand(DeleteEstablishment);

        private void DeleteEstablishment(object args)
        {
            if (args is Establishment establishment)
            {
                StadsAppRestApiClient.Instance.DeleteEstablishmentAsync(establishment.EstablishmentId);
                Establishments.Remove(establishment);
            }
        }

        public ICommand AddEstablishmentCommand => new RelayCommand(o => AddEstablishment());

        private void AddEstablishment()
        {
            Frame.Navigate(typeof(AddEstablishment), Store.StoreId);
        }

        public EntrepreneurViewModel()
        {
            _userManager = new UserManager();
        }

        public async Task LoadDataAsync()
        {
            Categories = await GetCategoriesAsync();
            Store = await GetStoreAsync();
            Establishments = new ObservableCollection<Establishment>(await GetEstablishmentsAsync());
        }

        #region Data Loaders

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Category>("Categories");
        }

        private async Task<Store> GetStoreAsync()
        {
            var store = await StadsAppRestApiClient.Instance.GetSingleObjectAsync<Store>(
                $"Stores/{_userManager.CurrentUser.StoreId}");
            StoreName = store.Name;
            StoreDescription = store.Description;
            SelectedCategory = Categories.Find(c => c.CategoryId == store.Category.CategoryId);
            return store;
        }

        private async Task<List<Establishment>> GetEstablishmentsAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Establishment>(
                $"Establishments/ForStore/{Store.StoreId}");
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}