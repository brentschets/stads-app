using Windows.UI.Xaml.Controls;
using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;
using Stads_App.Models;
using Stads_App.Views.Details;

namespace Stads_App.Views
{
    public sealed partial class Events
    {
        public override string Header { get; protected set; } = "Evenementen";

        private readonly EventsViewModel _viewModel;

        public Events()
        {
            InitializeComponent();
            _viewModel = new EventsViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
        }

        private void Details(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ListView) sender).SelectedItem;
            if (selectedItem != null)
                Frame.Navigate(typeof(EventDetails), ((Event) selectedItem).EventId);
        }
    }
}