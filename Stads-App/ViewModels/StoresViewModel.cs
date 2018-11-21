using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Models;
using Stads_App.Annotations;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public sealed class StoresViewModel : INotifyPropertyChanged
    {
        private List<Store> _stores;

        public List<Store> Stores
        {
            get => _stores;
            private set
            {
                _stores = value;
                OnPropertyChanged(nameof(Stores));
            }
        }

        private List<Store> AllStores { get; set; }

        private bool _isLoaded;

        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        private bool _isLimitedList;

        public bool IsLimitedList
        {
            get => _isLimitedList;
            private set
            {
                _isLimitedList = value;
                OnPropertyChanged(nameof(IsLimitedList));
            }
        }

        public ICommand SearchCommand => new RelayCommand(Search);

        public ICommand ShowAllStoresCommand
        {
            get { return new RelayCommand(o => ShowAllStores()); }
        }

        public StoresViewModel()
        {
            IsLoaded = false;
        }

        private async void ShowAllStores()
        {
            IsLoaded = false;
            Stores = null;
            await LoadDataAsync(null);
        }

        private static async Task<List<Store>> GetStoresAsync(int? categoryId)
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Store>(categoryId != null
                ? $"Stores/ByCategory/{categoryId}"
                : "Stores");
        }

        private void Search(object o)
        {
            var args = o as AutoSuggestBoxQuerySubmittedEventArgs;
            Stores = AllStores.FindAll(s => args != null && s.Name.ToLower().Contains(args.QueryText.ToLower()));
        }

        public async Task LoadDataAsync(int? categoryId)
        {
            AllStores = await GetStoresAsync(categoryId);
            if (Stores == null) Stores = AllStores;
            IsLimitedList = categoryId != null;
            IsLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}