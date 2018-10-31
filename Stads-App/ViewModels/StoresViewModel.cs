using System.Collections.Generic;
using System.Threading.Tasks;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public class StoresViewModel
    {
        private static readonly StadsAppRestApiClient Client = new StadsAppRestApiClient();

        public List<Store> Stores { get; }

        public StoresViewModel()
        {
            Stores = new List<Store>();
        }

        private static async Task<List<Store>> GetStoresAsync()
        {
            return await Client.GetListAsync<Store>("Stores");
        }

        public async Task LoadDataAsync()
        {
            if (Stores?.Count == 0) Stores.AddRange(await GetStoresAsync());
        }
    }
}