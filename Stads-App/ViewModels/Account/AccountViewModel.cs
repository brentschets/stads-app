using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Annotations;
using Stads_App.Models;
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
        private ObservableCollection<Establishment> _subscriptions;
        private bool _isLoaded;

        public ICommand LogoutCommand => new RelayCommand(o => LogoutUser());

        public ICommand UpdateCommand => new RelayCommand(o => UpdateUserAsync());

        public ICommand UnsubscribeCommand => new RelayCommand(Unsubscribe);

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

        public ObservableCollection<Establishment> Subscriptions
        {
            get => _subscriptions;
            private set
            {
                _subscriptions = value;
                OnPropertyChanged(nameof(Subscriptions));
            }
        }

        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        public AccountViewModel()
        {
            _userManager = new UserManager();
        }

        public async Task LoadDataAsync()
        {
            var user = UserManager.CurrentUser;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Subscriptions = new ObservableCollection<Establishment>(
                await StadsAppRestApiClient.Instance.GetListAsync<Establishment>(
                    $"Establishments/ForUser/{user.UserId}"));
            IsLoaded = true;
        }

        private void LogoutUser()
        {
            _userManager.Logout();
            Frame.Navigate(typeof(Login));
        }

        private async void UpdateUserAsync()
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

            var result = await _userManager.UpdateAsync(user);
            if (!result.Success) ErrorMsg = result.Error.Message;
        }

        private async void Unsubscribe(object args)
        {
            var establishmentId = (int) args;

            var user = UserManager.CurrentUser;

            var result = await _userManager.UnsubscribeAsync(user.UserId, establishmentId);

            if (!result.Success)
            {
                ErrorMsg = result.Error.Message;
                return;
            }

            var establishment = Subscriptions.First(e => e.EstablishmentId == establishmentId);
            Subscriptions.Remove(establishment);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}