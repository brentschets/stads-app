using Stads_App.Models;
using Stads_App.Annotations;
using Stads_App.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Stads_App.ViewModels
{
    public sealed class EventsViewModel : INotifyPropertyChanged
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

        private List<Event> AllEvents { get; set; }

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

        public ICommand SearchCommand => new RelayCommand(Search);

        public EventsViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Event>> GetEvents()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Event>("Events");
        }

        public async Task LoadDataAsync()
        {
            AllEvents = await GetEvents();
            if (Events == null) Events = AllEvents;
            IsLoaded = true;
        }

        public void Search(object o)
        {
            var args = o as AutoSuggestBoxQuerySubmittedEventArgs;
            Events = AllEvents.FindAll(s => args != null && s.Name.ToLower().Contains(args.QueryText.ToLower()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}