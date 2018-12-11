﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Navigation;
using Stads_App.Annotations;
using Stads_App.ViewModels.Account;

namespace Stads_App.Views.Account
{
    public sealed partial class Account: INotifyPropertyChanged
    {
        private string _header;
        private readonly AccountViewModel _viewModel;

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
            _viewModel = new AccountViewModel();
            DataContext = _viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.Frame = Frame;
            await _viewModel.LoadDataAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}