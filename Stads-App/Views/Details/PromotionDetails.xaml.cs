using Windows.UI.Xaml.Navigation;
using Stads_App.Models;

namespace Stads_App.Views.Details
{
    public sealed partial class PromotionDetails
    {
        public override string Header { get; set; }

        public PromotionDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var promotion = (Promotion) e.Parameter;
            if (promotion == null) return;
            Header = promotion.Name;
            DataContext = promotion;
        }
    }
}
