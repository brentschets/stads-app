using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Stads_App.Models;
using Stads_App.Annotations;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public class PromotionsViewModel : INotifyPropertyChanged
    {
        private List<Promotion> _promotions;

        public List<Promotion> Promotions
        {
            get => _promotions;
            private set
            {
                _promotions = value;
                OnPropertyChanged(nameof(Promotions));
            }
        }

        private List<Promotion> AllPromotions { get; set; }

        private bool _isLoaded;

        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        public ICommand SearchCommand => new RelayCommand(Search);

        public PromotionsViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Promotion>> GetPromotions()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Promotion>("Promotions");
        }

        public void Search(object o)
        {
            var args = o as AutoSuggestBoxQuerySubmittedEventArgs;
            Promotions =
                AllPromotions.FindAll(s => args != null && s.Name.ToLower().Contains(args.QueryText.ToLower()));
        }

        public async Task LoadDataAsync()
        {
            AllPromotions = await GetPromotions();
            if (Promotions == null) Promotions = AllPromotions;
            IsLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}