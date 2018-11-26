using Stads_App.Views.Register;

namespace Stads_App.Views
{
    public sealed partial class Account
    {
        public override string Header { get; protected set; } = "Account";

        public Account()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterUser));
        }
    }
}