using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;
using Stads_App.Views.Account;

namespace Stads_App.ViewModels.UserControls
{
    public sealed class AccountEditViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        public Frame Frame { private get; set; }

        public ICommand UpdateCommand => new RelayCommand(o => Update());

        public ICommand LogoutCommand => new RelayCommand(o => Logout());

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

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public AccountEditViewModel()
        {
            _userManager = new UserManager();
            if (!_userManager.IsLoggedIn()) throw new InvalidOperationException("No user is logged in");
            var user = _userManager.CurrentUser;

            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
        }

        private async void Update()
        {
            var user = _userManager.CurrentUser;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Username = Username;
            user.Password = Password;

            if (user.EqualsForUpdate(_userManager.CurrentUser) && string.IsNullOrWhiteSpace(Password))
            {
                ErrorMsg = "Geen veranderingen";
                return;
            }

            var result = await _userManager.UpdateAsync(user);
            ErrorMsg = result.Success ? string.Empty : result.Error?.Message;
        }

        private void Logout()
        {
            _userManager.Logout();

            var current = Frame.CurrentSourcePageType;
            Frame.Navigate(typeof(Login));

            // disable going back to account page
            foreach (var pageStackEntry in Frame.BackStack)
            {
                if (pageStackEntry.SourcePageType == current) Frame.BackStack.Remove(pageStackEntry);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}