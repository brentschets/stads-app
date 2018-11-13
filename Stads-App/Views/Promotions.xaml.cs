using Stads_App.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace Stads_App.Views
{
    public sealed partial class Promotions
    {
        public override string Header => "Promoties";

        private readonly PromotionViewModel _viewModel;

        public Promotions()
        {
            InitializeComponent();
            _viewModel = new PromotionViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadDataAsync();
        }
    }
}