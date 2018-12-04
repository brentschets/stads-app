using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class RegisterUser
    {
        public override string Header { get; protected set; } = "Registreren";

        private readonly RegisterUserViewModel _viewModel;

        public RegisterUser()
        {
            InitializeComponent();
            _viewModel = new RegisterUserViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.Frame = Frame;
        }
    }
}