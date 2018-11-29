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
    public sealed partial class Home
    {
        public override string Header { get; protected set; } = "Home";

        private readonly HomeViewModel _viewModel;

        private Establishment _selectedEstablishment;

        public Home()
        {
            InitializeComponent();
            // Ensure the page is only created once, and cached during navigation (required for back connected animation)
            NavigationCacheMode = NavigationCacheMode.Enabled;
            _viewModel = new HomeViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_viewModel.IsLoaded == false)
                await _viewModel.LoadDataAsync();
        }

        private void EstablishmentDetails(object sender, ItemClickEventArgs e)
        {
            if (!(PopularStoresCollection.ContainerFromItem(e.ClickedItem) is ListViewItem container)) return;
            _selectedEstablishment = container.Content as Establishment;
            PopularStoresCollection.PrepareConnectedAnimation("StoreImgForwardConnectedAnimation", _selectedEstablishment,
                "StoreImg");
            PopularStoresCollection.PrepareConnectedAnimation("StoreNameForwardConnectedAnimation", _selectedEstablishment,
                "StoreName");
            if (_selectedEstablishment != null) Frame.Navigate(typeof(EstablishmentDetails), _selectedEstablishment);
        }

        private void EventDetails(object sender, ItemClickEventArgs e)
        {
            if (!(PopularEventsCollection.ContainerFromItem(e.ClickedItem) is ListViewItem container)) return;
            if (container.Content is Event selectedEvent) Frame.Navigate(typeof(EventDetails), selectedEvent);
        }

        private void PromotionDetails(object sender, ItemClickEventArgs e)
        {
            if (!(PopularPromotionsCollection.ContainerFromItem(e.ClickedItem) is ListViewItem container)) return;
            if (container.Content is Promotion selectedPromotion)
                Frame.Navigate(typeof(PromotionDetails), selectedPromotion);
        }

        private async void PopularStoresCollection_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_selectedEstablishment == null) return;
            PopularStoresCollection.ScrollIntoView(_selectedEstablishment, ScrollIntoViewAlignment.Default);
            // undo focus
            PopularStoresCollection.SelectedItem = null;
            PopularStoresCollection.UpdateLayout();
            var storeImgBackConnectedAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("StoreImgBackConnectedAnimation");
            if (storeImgBackConnectedAnimation != null)
            {
                await PopularStoresCollection.TryStartConnectedAnimationAsync(storeImgBackConnectedAnimation,
                    _selectedEstablishment,
                    "StoreImg");
            }

            var storeNameBackConnectedAnimation = ConnectedAnimationService.GetForCurrentView()
                .GetAnimation("StoreNameBackConnectedAnimation");
            if (storeNameBackConnectedAnimation != null)
            {
                await PopularStoresCollection.TryStartConnectedAnimationAsync(storeNameBackConnectedAnimation,
                    _selectedEstablishment, "StoreName");
            }
        }
    }
}