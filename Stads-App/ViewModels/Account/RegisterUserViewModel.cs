using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Stads_App.Annotations;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Account
{
    public sealed class RegisterUserViewModel : INotifyPropertyChanged
    {
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

        public ICommand RegisterCommand => new RelayCommand(o => RegisterUser());

        private readonly UserManager _userManager;

        public RegisterUserViewModel()
        {
            _userManager = new UserManager();
        }

        private void RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMsg = "Paswoord kan niet leeg zijn";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMsg = "Paswoorden komen niet overeen";
                return;
            }

            var result = _userManager.Register(Username, Password, FirstName, LastName);
            if (result.Success) ErrorMsg = "Nieuwe gebruiker geregistreerd";
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
