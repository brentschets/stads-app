using Windows.UI.Xaml.Controls;
using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;
using Stads_App.Models;
using Stads_App.Views.Details;

namespace Stads_App.Views
{
    public sealed partial class Promotions
    {
        public override string Header { get; protected set; } = "Promoties";

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

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ListView) sender).SelectedItem;
            if (selectedItem != null)
                Frame.Navigate(typeof(PromotionDetails), selectedItem as Promotion);
        }
    }
}