
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Stads_App.Annotations;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class Account: INotifyPropertyChanged
    {
        private string _header;

        public override string Header
        {
            get => _header;
            protected set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public Account()
        {
            InitializeComponent();
            DataContext = new AccountViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
