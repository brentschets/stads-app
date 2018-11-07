using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public sealed class StoresViewModel : INotifyPropertyChanged
    {
        private static readonly StadsAppRestApiClient Client = new StadsAppRestApiClient();

        public List<Store> Stores { get; }

        private bool _isLoaded;

        public bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        public StoresViewModel()
        {
            Stores = new List<Store>();
            IsLoaded = false;
        }

        private static async Task<List<Store>> GetStoresAsync()
        {
            return await Client.GetListAsync<Store>("Stores");
        }

        public async Task LoadDataAsync()
        {
            if (Stores?.Count == 0)
            {
                Stores.AddRange(await GetStoresAsync());
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