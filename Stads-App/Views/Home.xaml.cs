﻿using System;
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

        private Store _selectedStore;

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

        private void StoreDetails(object sender, ItemClickEventArgs e)
        {
            if (!(PopularStoresCollection.ContainerFromItem(e.ClickedItem) is ListViewItem container)) return;
            _selectedStore = container.Content as Store;
            PopularStoresCollection.PrepareConnectedAnimation("ForwardConnectedAnimation", _selectedStore, "StoreImg");
            if (_selectedStore != null) Frame.Navigate(typeof(StoreDetails), _selectedStore.StoreId);
        }

        private void EventDetails(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ListView) sender).SelectedItem;
            if (selectedItem != null)
                Frame.Navigate(typeof(EventDetails), ((Event) selectedItem).EventId);
        }

        private void PromotionDetails(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ListView) sender).SelectedItem;
            if (selectedItem != null)
                Frame.Navigate(typeof(PromotionDetails), ((Promotion) selectedItem).PromotionId);
        }

        private async void PopularStoresCollection_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_selectedStore == null) return;
            PopularStoresCollection.ScrollIntoView(_selectedStore, ScrollIntoViewAlignment.Default);
            // undo focus
            PopularStoresCollection.SelectedItem = null;
            PopularStoresCollection.UpdateLayout();
            var connectedAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("BackConnectedAnimation");
            if (connectedAnimation != null)
            {
                await PopularStoresCollection.TryStartConnectedAnimationAsync(connectedAnimation, _selectedStore,
                    "StoreImg");
            }
        }
    }
}