﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Stads_App.Utils.Authentication;
using Stads_App.Views;
using Stads_App.Views.Account;

namespace Stads_App
{
    public sealed partial class MainPage
    {
        private event UserManager.ChangeEvent AccountChangedEvent;

        private event UserManager.UserUpdateEvent UserUpdateEvent;

        public MainPage()
        {
            InitializeComponent();

            var userManager = new UserManager();

            AccountChangedEvent = u =>
            {
                var loginPage = ("account", typeof(Login));
                var accountPage = ("account", typeof(Account));
                var entrepreneurPage = ("account", typeof(Entrepreneur));
                if (userManager.IsLoggedIn())
                {
                    AccountNavViewItem.Content = u.Username;
                    _pages.Remove(loginPage);
                    _pages.Add(userManager.IsEntrepreneur() ? entrepreneurPage : accountPage);
                }
                else if (_pages.Contains(entrepreneurPage) || _pages.Contains(accountPage))
                {
                    AccountNavViewItem.Content = "Aanmelden";
                    _pages.Remove(_pages.Contains(entrepreneurPage) ? entrepreneurPage : accountPage);
                    _pages.Add(loginPage);
                }
            };
            UserUpdateEvent = u => AccountNavViewItem.Content = u.Username;
            UserManager.AccountChanged += AccountChangedEvent;
            UserManager.UserUpdated += UserUpdateEvent;
        }

        ~MainPage()
        {
            UserManager.AccountChanged -= AccountChangedEvent;
            UserManager.UserUpdated -= UserUpdateEvent;
        }

        private Type _currentPage;

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page 
        private readonly IList<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(Home)),
            ("stores", typeof(Stores)),
            ("categories", typeof(Categories)),
            ("promotions", typeof(Promotions)),
            ("events", typeof(Events)),
            ("account", typeof(Login))
        };

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += NavView_BackRequested;

            // NavView doesn't load any page by default, specify Home
            NavView_Navigate("home");
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem == null)
                return;

            // Getting the Tag from Content (args.InvokedItem is the content of NavigationViewItem)
            var navItemTag = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(i => args.InvokedItem.Equals(i.Content))
                .Tag.ToString();

            NavView_Navigate(navItemTag);
        }

        private void NavView_FooterItemTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!(sender is NavigationViewItem navViewItem)) return;
            var navItemTag = navViewItem.Tag as string;
            NavView_Navigate(navItemTag);
        }

        private void NavView_Navigate(string navItemTag)
        {
            var item = _pages.First(p => p.Tag.Equals(navItemTag));
            if (_currentPage == item.Page)
                return;
            ContentFrame.Navigate(item.Page);

            _currentPage = item.Page;
        }

        private void NavView_BackRequested(object sender, BackRequestedEventArgs args) =>
            On_BackRequested();

        private void On_BackRequested()
        {
            if (!ContentFrame.CanGoBack) return;

            // Don't go back if the nav pane is overlayed
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return;

            ContentFrame.GoBack();
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ContentFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

            if (item.Tag == null) return;
            var selectedItem = NavView.MenuItems.OfType<NavigationViewItem>()
                .FirstOrDefault(n => n.Tag.Equals(item.Tag));
            if (selectedItem == null)
            {
                // clear selection
                NavView.SelectedItem = HiddenItem;
            }

            NavView.SelectedItem = selectedItem;
            if (e.SourcePageType != null && _currentPage != e.SourcePageType) _currentPage = e.SourcePageType;
        }
    }
}