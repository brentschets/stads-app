using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Stads_App.Annotations;
using Stads_App.Models;

namespace Stads_App.Views.Details
{
    public sealed partial class StoreDetails : INotifyPropertyChanged
    {
        private string _header;

        private static readonly IEnumerable<Type> AnimatedViews = new List<Type>
        {
            typeof(Stores),
            typeof(Home)
        };

        public override string Header
        {
            get => _header;
            protected set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public StoreDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null) return;
            if (e.Parameter is Store store)
            {
                Header = store.Name;
                DataContext = store;
            }

            var connectedAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            connectedAnimation?.TryStart(DetailedImg);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back && AnimatedViews.Contains(e.SourcePageType))
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackConnectedAnimation", DetailedImg);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}