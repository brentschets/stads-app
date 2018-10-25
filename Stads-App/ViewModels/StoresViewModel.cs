using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stads_App.Models;

namespace Stads_App.ViewModels
{
    public class StoresViewModel
    {
        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get => _stores ?? (_stores = GetStores());
            set => _stores = value;
        }


        private ObservableCollection<Store> GetStores()
        {
            var client = new HttpClient();
            return JsonConvert.DeserializeObject<ObservableCollection<Store>>(
                client.GetStringAsync("https://stadsapprestapi.azurewebsites.net/api/stores").Result);
        }
    }
}