using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Stads_App.Models;
using Stads_App.Properties;
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

        public StoresViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Store>> GetStoresAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Store>("Stores");
        }

        public void Search(string searchString)
        {
            Stores = AllStores.FindAll(s => s.Name.ToLower().Contains(searchString.ToLower()));
        }

        public async Task LoadDataAsync()
        {
            AllStores = await GetStoresAsync();
            if (Stores == null) Stores = AllStores;
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