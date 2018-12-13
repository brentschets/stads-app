using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class EntrepreneurRegister
    {
        public override string Header { get; protected set; } = "Handelaar Registreren";

        private readonly RegisterEntrepreneurViewModel _viewModel;

        public EntrepreneurRegister()
        {
            InitializeComponent();
            _viewModel = new RegisterEntrepreneurViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
        }
    }
}