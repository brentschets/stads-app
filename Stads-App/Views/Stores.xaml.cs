using Stads_App.ViewModels;

namespace Stads_App.Views
{
    public sealed partial class Stores
    {
        public override string Header => "Winkels";

        public Stores()
        {
            InitializeComponent();
            DataContext = new StoresViewModel();
        }
    }
}
