using System;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Stads_App.Models;
using Stads_App.ViewModels;
using Stads_App.Views.Details;

namespace Stads_App.Views
{
    public sealed partial class Stores
    {
        public override string Header { get; protected set; } = "Winkels";

        private readonly StoresViewModel _viewModel;

        private Store _selectedStore;

        public Stores()
        {
            InitializeComponent();
            _viewModel = new StoresViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null) await _viewModel.LoadDataAsync((int) e.Parameter);
            else await _viewModel.LoadDataAsync(null);
        }

        private void Details(object sender, ItemClickEventArgs e)
        {
            var container = StoresCollection.ContainerFromItem(e.ClickedItem) as ListViewItem;
            if (container != null)
            {
                _selectedStore = container.Content as Store;
                StoresCollection.PrepareConnectedAnimation("ForwardConnectedAnimation", _selectedStore, "storeImg");
                Frame.Navigate(typeof(StoreDetails), _selectedStore.StoreId);
            }
        }
    }
}