using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Account
{
    public sealed class LoginViewModel : INotifyPropertyChanged
    {
        private readonly Frame _frame;

        private readonly UserManager _userManager;

        private string _errorMsg;

        public string ErrorMsg
        {
            get => _errorMsg;
            private set
            {
                _errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(o => Login());

        public LoginViewModel(Frame parentFrame)
        {
            _frame = parentFrame;
            _userManager = new UserManager();
        }

        private void Login()
        {
            var result = _userManager.Authenticate(Username, Password);
            if (result.Success) _frame.Navigate(typepof(Account));
            ErrorMsg = result.Error.Message;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}