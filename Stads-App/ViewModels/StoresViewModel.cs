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

        private List<Store> _allStores;

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

        private int? _categoryId;

        public ICommand SearchCommand => new RelayCommand(Search);

        public ICommand ShowAllStoresCommand => new RelayCommand(ShowAllStores);

        private void Search(object o)
        {
            var args = o as AutoSuggestBoxQuerySubmittedEventArgs;
            Stores = _allStores.FindAll(s => args != null && s.Name.ToLower().Contains(args.QueryText.ToLower()));
        }

        private async void ShowAllStores(object o)
        {
            _allStores = await StadsAppRestApiClient.Instance.GetListAsync<Store>("Stores");
            Stores = _allStores;
            IsLimitedList = false;
        }

        public async Task LoadDataAsync(int? categoryId)
        {
            if (categoryId != null && categoryId != _categoryId)
            {
                _allStores =
                    await StadsAppRestApiClient.Instance.GetListAsync<Store>($"Stores/ByCategory/{categoryId}");
                Stores = _allStores;
                _categoryId = categoryId;
                IsLimitedList = true;
                IsLoaded = true;
            }
            else if (categoryId == null && null != _categoryId || Stores == null)
            {
                _allStores = await StadsAppRestApiClient.Instance.GetListAsync<Store>("Stores");
                Stores = _allStores;
                _categoryId = null;
                IsLimitedList = false;
                IsLoaded = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}