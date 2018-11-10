using Stads_App.Models;
using Stads_App.Properties;
using Stads_App.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Stads_App.ViewModels
{
    class EventViewModel
    {
        private List<Store> _event;

        public List<Store> Events
        {
            get => _event;
            private set
            {
                _event = value;
                OnPropertyChanged(nameof(Events));
            }
        }

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

        public EventViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Store>> GetEvents()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Store>("Stores/Events");
        }

        public async Task LoadDataAsync()
        {
            Events = await GetEvents();
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
