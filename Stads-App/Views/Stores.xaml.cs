using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels;

namespace Stads_App.Views
{
    public sealed partial class Stores
    {
        public override string Header => "Winkels";

        private readonly StoresViewModel _viewModel = new StoresViewModel();

        public Stores()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
            StoresListView.Visibility = Visibility.Visible;
            ProgressRing.IsActive = false;
        }
    }
}
