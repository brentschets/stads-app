using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Stads_App.Models;
using Stads_App.Views.Details;

namespace Stads_App.Views.Custom
{
    public sealed partial class InlineStoreOverview
    {
        public Store Store { get; set; }

        public Frame Frame { get; set; }

        public InlineStoreOverview()
        {
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = Store;
        }

        private void GoToStore(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(StoreDetails), Store.StoreId);
        }
    }
}
