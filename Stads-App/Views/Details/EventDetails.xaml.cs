using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Navigation;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.Views.Details
{
    public sealed partial class EventDetails : INotifyPropertyChanged
    {
        private string _header;

        public override string Header
        {
            get => _header;
            protected set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public EventDetails()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null) return;
            var eventId = (int) e.Parameter;
            var _event = await StadsAppRestApiClient.Instance.GetSingleObjectAsync<Event>($"Events/{eventId}");
            Header = _event.Name;
            DataContext = _event;
            InlineStoreOverview.Store = _event.Store;
            InlineStoreOverview.Frame = Frame;
            InlineStoreOverview.SetDataContext();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}