using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public class StoresViewModel
    {
        public IEnumerable<Store> Stores { get; private set; }

        public IEnumerable<Store> MostVisited { get; private set; }

        private static async Task<IEnumerable<Store>> GetStoresAsync()
        {
            var client = new StadsAppRestApiClient();
            return await client.GetListAsync<Store>("Stores");
        }

        private static async Task<IEnumerable<Store>> GetPopularAsync()
        {
            var client = new StadsAppRestApiClient();
            return await client.GetListAsync<Store>("Stores/MostVisited");
        }

        public async Task LoadData()
        {
            Stores = await GetStoresAsync();
            MostVisited = await GetPopularAsync();
        }
    }
}