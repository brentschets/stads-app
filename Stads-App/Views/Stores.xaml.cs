using System;
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
            // Ensure the page is only created once, and cached during navigation (required for back connected animation)
            NavigationCacheMode = NavigationCacheMode.Enabled;
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
            if (!(StoresCollection.ContainerFromItem(e.ClickedItem) is ListViewItem container)) return;
            _selectedStore = container.Content as Store;
            StoresCollection.PrepareConnectedAnimation("StoreImgForwardConnectedAnimation", _selectedStore, "StoreImg");
            StoresCollection.PrepareConnectedAnimation("StoreNameForwardConnectedAnimation", _selectedStore,
                "StoreName");
            if (_selectedStore != null) Frame.Navigate(typeof(StoreDetails), _selectedStore);
        }

        private async void StoresCollection_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_selectedStore == null) return;
            StoresCollection.ScrollIntoView(_selectedStore, ScrollIntoViewAlignment.Default);
            // undo focus
            StoresCollection.SelectedItem = null;
            StoresCollection.UpdateLayout();
            var storeImgBackConnectedAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("StoreImgBackConnectedAnimation");
            if (storeImgBackConnectedAnimation != null)
            {
                await StoresCollection.TryStartConnectedAnimationAsync(storeImgBackConnectedAnimation, _selectedStore,
                    "StoreImg");
            }

            var storeNameBackConnectedAnimation = ConnectedAnimationService.GetForCurrentView()
                .GetAnimation("StoreNameBackConnectedAnimation");
            if (storeNameBackConnectedAnimation != null)
            {
                await StoresCollection.TryStartConnectedAnimationAsync(storeNameBackConnectedAnimation, _selectedStore,
                    "StoreName");
            }
        }
    }
}