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
    public class AccountViewModel : INotifyPropertyChanged
    {
        public Frame Frame { private get; set; }
        private readonly UserManager _userManager;
        private string _username;
        private string _firstName;
        private string _lastName;

        public ICommand LogoutCommand => new RelayCommand(o => LogoutUser());

        public string LastName
        {
            get => _lastName;
            private set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }


        public string FirstName
        {
            get => _firstName;
            private set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }


        public string Username
        {
            get => _username;
            private set
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

        public void LogoutUser()
        {
            _userManager.Logout();
            Frame.Navigate(typeof(Login));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
