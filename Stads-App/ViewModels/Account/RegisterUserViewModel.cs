﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;
using Stads_App.Views.Account;

namespace Stads_App.ViewModels.Account
{
    public sealed class RegisterUserViewModel : INotifyPropertyChanged
    {
        public Frame Frame { private get; set; }

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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public ICommand RegisterCommand => new RelayCommand(o => RegisterUserAsync());

        public ICommand EntrepreneurRegister => new RelayCommand( o => GoToRegisterEntrepeneur());

        private readonly UserManager _userManager;

        public RegisterUserViewModel()
        {
            _userManager = new UserManager();
        }

        public void GoToRegisterEntrepeneur()
        {
            Frame.Navigate(typeof(EntrepreneurRegister));
        }

        private async void RegisterUserAsync()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMsg = "Wachtwoord kan niet leeg zijn";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMsg = "Wachtwoorden komen niet overeen";
                return;
            }

            var result = await _userManager.RegisterAsync(Username, Password, FirstName, LastName);
            if (result.Success) Frame.Navigate(typeof(Login));
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
