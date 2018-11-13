using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace Stads_App.Views
{
    public sealed partial class Events
    {
        public override string Header => "Evenementen";

        private readonly EventViewModel _viewModel;

        public Events()
        {
            InitializeComponent();
            _viewModel = new EventViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
        }
    }
}