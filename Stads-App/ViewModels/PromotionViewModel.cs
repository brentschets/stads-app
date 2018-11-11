using Stads_App.Models;
using Stads_App.Properties;
using Stads_App.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Stads_App.ViewModels
{
    class PromotionViewModel : INotifyPropertyChanged
    {
        private List<Store> _promotion;

        public List<Store> Promotions
        {
            get => _promotion;
            private set
            {
                _promotion = value;
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

        public PromotionViewModel()
        {
            IsLoaded = false;
        }

        private static async Task<List<Store>> GetPromotions()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Store>("Stores/Promotion");
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

