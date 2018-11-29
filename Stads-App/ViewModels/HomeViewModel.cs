using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Stads_App.Models;
using Stads_App.Annotations;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public sealed class HomeViewModel : INotifyPropertyChanged
    {
        private List<Establishment> _popularEstablishments;

        public List<Establishment> PopularEstablishments
        {
            get => _popularEstablishments;
            private set
            {
                _popularEstablishments = value;
                OnPropertyChanged(nameof(PopularEstablishments));
            }
        }

        private List<Event> _popularEvents;

        public List<Event> PopularEvents
        {
            get => _popularEvents;
            private set
            {
                _popularEvents = value;
                OnPropertyChanged(nameof(PopularEvents));
            }
        }

        private List<Promotion> _popularPromotions;

        public List<Promotion> PopularPromotions
        {
            get => _popularPromotions;
            private set
            {
                _popularPromotions = value;
                OnPropertyChanged(nameof(PopularPromotions));
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

        public HomeViewModel()
        {
            IsLoaded = false;
        }

        #region Data loaders
        private static async Task<List<Establishment>> GetPopularEstablishmentsAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Establishment>("Establishments/Popular/10");
        }

        private static async Task<List<Event>> GetPopularEventsAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Event>("Events/Popular/10");
        }

        private static async Task<List<Promotion>> GetPopularPromotionsAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Promotion>("Promotions/Popular/10");
        }
        #endregion

        public async Task LoadDataAsync()
        {
            PopularEstablishments = await GetPopularEstablishmentsAsync();
            PopularEvents = await GetPopularEventsAsync();
            PopularPromotions = await GetPopularPromotionsAsync();
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