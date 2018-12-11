using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class EntrepreneurRegister
    {
        public override string Header { get; protected set; } = "Handelaar Registreren";

        private readonly RegisterEntrepreneurViewModel _viewModel;

        public EntrepreneurRegister()
        {
            this.InitializeComponent();
        _viewModel = new RegisterEntrepreneurViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _viewModel.LoadData();
        }
    }
}
