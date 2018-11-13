using Stads_App.Models;
using Stads_App.Properties;
using Stads_App.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Stads_App.ViewModels
{
    public sealed class EventViewModel : INotifyPropertyChanged
    {
        private List<Event> _event;

        public List<Event> Events
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

        private static async Task<List<Event>> GetEvents()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Event>("Events");
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
