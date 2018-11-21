using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Stads_App.Models;
using Stads_App.ViewModels;

namespace Stads_App.Views
{
    public sealed partial class Categories
    {
        public override string Header { get; protected set; } = "Categorieën";

        private readonly CategoriesViewModel _viewModel;

        public Categories()
        {
            InitializeComponent();
            _viewModel = new CategoriesViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadData();
        }

        private void StoresByCategory(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((GridView) sender).SelectedItem;
            if (selectedItem != null)
                Frame.Navigate(typeof(Stores), ((Category) selectedItem).CategoryId);
        }
    }
}