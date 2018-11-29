using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Stads_App.Annotations;
using Stads_App.Models;

namespace Stads_App.Views.Details
{
    public sealed partial class EstablishmentDetails : INotifyPropertyChanged
    {
        private string _header;

        public override string Header
        {
            get => _header;
            protected set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public EstablishmentDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null) return;
            if (e.Parameter is Establishment establishment)
            {
                Header = establishment.Store.Name;
                DataContext = establishment;
            }

            var storeImgForwaredConnectedAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("StoreImgForwardConnectedAnimation");
            storeImgForwaredConnectedAnimation?.TryStart(DetailedImg);
            var storeNameForwardConnectedAnimation = ConnectedAnimationService.GetForCurrentView()
                .GetAnimation("StoreNameForwardConnectedAnimation");
            storeNameForwardConnectedAnimation?.TryStart(DetailedName);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode != NavigationMode.Back || !AnimatedViews.All.Contains(e.SourcePageType)) return;
            ConnectedAnimationService.GetForCurrentView()
                .PrepareToAnimate("StoreImgBackConnectedAnimation", DetailedImg);
            ConnectedAnimationService.GetForCurrentView()
                .PrepareToAnimate("StoreNameBackConnectedAnimation", DetailedName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}