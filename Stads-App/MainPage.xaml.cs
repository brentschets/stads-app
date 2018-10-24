﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels;
using Stads_App.Views;

namespace Stads_App
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }

        private Type _currentPage;

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page 
        private readonly IList<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(Home)),
            ("stores", typeof(Stores))
        };

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;

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

        private void NavView_Navigate(string navItemTag)
        {
            var item = _pages.First(p => p.Tag.Equals(navItemTag));
            if (_currentPage == item.Page)
                return;
            ContentFrame.Navigate(item.Page);

            _currentPage = item.Page;
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) =>
            On_BackRequested();

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            var item = _pages.First(p => p.Page == e.SourcePageType);

            NavView.SelectedItem = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(item.Tag));
        }
    }
}