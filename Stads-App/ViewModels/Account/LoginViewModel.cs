using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;
using Stads_App.Views.Account;

namespace Stads_App.ViewModels.Account
{
    public sealed class LoginViewModel : INotifyPropertyChanged
    {
        public Frame Frame { private get; set; }

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

        public LoginViewModel()
        {
            _userManager = new UserManager();
        }

        private void Login()
        {
            var result = _userManager.Authenticate(Username, Password);
            if (result.Success)
            {
                Frame.Navigate(typeof(Views.Account.Account));
                // disable going back to login page
                foreach (var pageStackEntry in Frame.BackStack)
                {
                    if (pageStackEntry.SourcePageType == typeof(Login)) Frame.BackStack.Remove(pageStackEntry);
                }
            }
            else ErrorMsg = result.Error.Message;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}