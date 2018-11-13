using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels;

namespace Stads_App.Views
{
    public sealed partial class Stores
    {
        public override string Header => "Winkels";

        private readonly StoresViewModel _viewModel;

        public Stores()
        {
            InitializeComponent();
            _viewModel = new StoresViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
        }

        private void Search(AutoSuggestBox autoSuggestBox, AutoSuggestBoxQuerySubmittedEventArgs autoSuggestBoxQuerySubmittedEventArgs)
        {
            _viewModel.Search(autoSuggestBoxQuerySubmittedEventArgs.QueryText);
        }
    }
}
