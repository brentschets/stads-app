using System;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class AddEstablishment
    {
        public override string Header { get; protected set; } = "Vestiging toevoegen";

        private readonly AddEstablishmentViewModel _viewModel;

        public AddEstablishment()
        {
            InitializeComponent();
            _viewModel = new AddEstablishmentViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.Frame = Frame;
            if (e.Parameter == null) throw new InvalidOperationException("You must pass a store id");
            _viewModel.StoreId = (int) e.Parameter;
        }
    }
}