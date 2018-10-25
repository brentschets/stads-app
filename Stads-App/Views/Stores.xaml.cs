using System.Collections.ObjectModel;
using System.Net.Http;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Stads_App.Models;
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
