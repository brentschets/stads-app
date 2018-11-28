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
    public sealed class AccountViewModel : INotifyPropertyChanged
    {
        public Frame Frame { private get; set; }
        private readonly UserManager _userManager;

        private string _username;
        private string _firstName;
        private string _lastName;
        private string _errorMsg;

        public ICommand LogoutCommand => new RelayCommand(o => LogoutUser());

        public ICommand UpdateCommand => new RelayCommand(o => UpdateUser());

        public string ErrorMsg
        {
            get => _errorMsg;
            private set
            {
                _errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }


        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public AccountViewModel()
        {
            _userManager = new UserManager();
            var user = UserManager.CurrentUser;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
        }

        private void LogoutUser()
        {
            _userManager.Logout();
            Frame.Navigate(typeof(Login));
        }

        private void UpdateUser()
        {
            var user = UserManager.CurrentUser;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Username = Username;
            if (user.Equals(UserManager.CurrentUser))
            {
                ErrorMsg = "Geen aanpassingen";
                return;
            }
            var result = _userManager.Update(user);
            if (!result.Success) ErrorMsg = result.Error.Message;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}