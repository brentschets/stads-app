using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels;
using Stads_App.Views.Details;

namespace Stads_App.Views
{
    public sealed partial class Home
    {
        public override string Header => "Home";

        private readonly HomeViewModel _viewModel;

        public Home()
        {
            InitializeComponent();
            _viewModel = new HomeViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
        }

        private void Details(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(StoreDetails), ((ListView) sender).SelectedItem);
        }
    }
}