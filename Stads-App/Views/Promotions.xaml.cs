using Windows.UI.Xaml.Controls;
using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;
using Stads_App.Views.Details;

namespace Stads_App.Views
{
    public sealed partial class Promotions
    {
        protected override string Header { get; set; } = "Promoties";

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

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(PromotionDetails), ((ListView) sender).SelectedItem);
        }
    }
}