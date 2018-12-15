using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Navigation;
using Stads_App.Annotations;
using Stads_App.Utils.Authentication;
using Stads_App.ViewModels.Account;
using Stads_App.ViewModels.UserControls;

namespace Stads_App.Views.Account
{
    public sealed partial class Account : INotifyPropertyChanged
    {
        private string _header;
        private readonly AccountViewModel _viewModel;

        public override string Header
        {
            get => _header;
            protected set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public Account()
        {
            InitializeComponent();
            _viewModel = new AccountViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AccountEditUserControl.DataContext = new AccountEditViewModel {Frame = Frame};
            UserManager.UserUpdated += user => Header = user?.Username;
            Header = new UserManager().CurrentUser.Username;
            await _viewModel.LoadDataAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}