using Windows.UI.Xaml.Controls;
using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace Stads_App.Views
{
    public sealed partial class Promotions
    {
        public override string Header => "Promoties";

        private readonly PromotionsViewModel _viewModel;

        public Promotions()
        {
            InitializeComponent();
            _viewModel = new PromotionsViewModel();
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