using System.ComponentModel;
using System.Runtime.CompilerServices;
using Stads_App.Annotations;

namespace Stads_App.ViewModels.Account
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _firstName;
        private string _lastName;

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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
