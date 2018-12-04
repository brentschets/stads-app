using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class Login
    {
        public override string Header { get; protected set; } = "Aanmelden";

        private readonly LoginViewModel _viewModel;

        public Login()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.Frame = Frame;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterUser));
        }
    }
}