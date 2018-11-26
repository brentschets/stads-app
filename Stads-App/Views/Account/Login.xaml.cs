using Windows.UI.Xaml;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class Login
    {
        public override string Header { get; protected set; } = "Account";

        private readonly LoginViewModel _viewModel;

        public Login()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterUser));
        }
    }
}