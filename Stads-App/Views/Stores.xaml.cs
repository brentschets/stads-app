using Stads_App.ViewModels;

namespace Stads_App.Views
{
    public sealed partial class Stores
    {
        public Stores()
        {
            InitializeComponent();
            DataContext = new StoresViewModel();
        }
    }
}
