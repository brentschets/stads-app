using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Stads_App.Models;
using Stads_App.Properties;
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

        public PromotionsViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Promotion>> GetPromotions()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Promotion>("Promotions");
        }

        public async Task LoadDataAsync()
        {
            Promotions = await GetPromotions();
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