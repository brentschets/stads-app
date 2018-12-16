using Windows.UI.Xaml.Navigation;
using Stads_App.Models;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class EditEstablishment
    {
        public override string Header { get; protected set; } = "Vestiging aanpassen";

        private readonly EditEstablishmentViewModel _viewModel;

        public EditEstablishment()
        {
            InitializeComponent();
            _viewModel = new EditEstablishmentViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Establishment establishment)
            {
                _viewModel.Establishment = establishment;
                _viewModel.Frame = Frame;
            }
        }
    }
}