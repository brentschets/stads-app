using Windows.UI.Xaml.Controls;
using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;

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

        private void Search(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            _viewModel.Search(args.QueryText);
        }
    }
}