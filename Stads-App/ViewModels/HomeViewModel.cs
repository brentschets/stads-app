using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private List<Store> _mostVisited;

        public List<Store> MostVisited
        {
            get => _mostVisited;
            set
            {
                _mostVisited = value;
                OnPropertyChanged(nameof(MostVisited));
            }
        }

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

        public HomeViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Store>> GetMostVisitedAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Store>("Stores/MostVisited");
        }

        public async Task LoadDataAsync()
        {
            MostVisited = await GetMostVisitedAsync();
            IsLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}