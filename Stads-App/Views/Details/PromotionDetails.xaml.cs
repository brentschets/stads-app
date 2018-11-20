using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Navigation;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.Views.Details
{
    public sealed partial class PromotionDetails : INotifyPropertyChanged
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

        public PromotionDetails()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null) return;
            var promotionId = (int) e.Parameter;
            var promotion =
                await StadsAppRestApiClient.Instance.GetSingleObjectAsync<Promotion>($"Promotions/{promotionId}");
            Header = promotion.Name;
            DataContext = promotion;
            InlineStoreOverview.Frame = Frame;
            InlineStoreOverview.Store = promotion.Store;
            InlineStoreOverview.SetDataContext();
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}