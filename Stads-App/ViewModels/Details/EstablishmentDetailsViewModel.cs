using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;
using Stads_App.Utils.Authentication;

namespace Stads_App.ViewModels.Details
{
    public sealed class EstablishmentDetailsViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        public bool IsNotSubscribed =>
            _userManager.IsLoggedIn() && !_userManager.IsSubscribed(Establishment.EstablishmentId);

        public Establishment Establishment { get; }

        public ICommand SubscribeCommand => new RelayCommand(SubscribeAsync);

        public EstablishmentDetailsViewModel(Establishment establishment)
        {
            _userManager = new UserManager();
            Establishment = establishment;
        }

        private async void SubscribeAsync(object args)
        {
            var result = await _userManager.SubscribeAsync(Establishment.EstablishmentId);
            if (result.Success) OnPropertyChanged(nameof(IsNotSubscribed));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}