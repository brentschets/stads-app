using Windows.UI.Xaml.Navigation;
using Stads_App.Models;

namespace Stads_App.Views.Details
{
    public sealed partial class StoreDetails
    {
        public override string Header { get; set; }

        public StoreDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var store = (Store) e.Parameter;
            if (store == null) return;
            Header = store.Name;
            DataContext = store;
        }
    }
}