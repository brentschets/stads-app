using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Account
{
    public sealed class AccountViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        public ICommand UnsubscribeCommand => new RelayCommand(Unsubscribe);

        private ObservableCollection<Establishment> _subscriptions;

        public ObservableCollection<Establishment> Subscriptions
        {
            get => _subscriptions;
            private set
            {
                _subscriptions = value;
                OnPropertyChanged(nameof(Subscriptions));
            }
        }

        public AccountViewModel()
        {
            _userManager = new UserManager();
        }

        public async Task LoadDataAsync()
        {
            var user = _userManager.CurrentUser;
            Subscriptions = new ObservableCollection<Establishment>(
                await StadsAppRestApiClient.Instance.GetListAsync<Establishment>(
                    $"Establishments/ForUser/{user.UserId}"));
        }

        private async void Unsubscribe(object args)
        {
            var establishmentId = (int) args;

            var result = await _userManager.UnsubscribeAsync(establishmentId);

            if (!result.Success) return;

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