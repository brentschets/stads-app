using System.Collections.Generic;
using System.Threading.Tasks;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public class StoresViewModel
    {
        public List<Store> Stores { get; } = new List<Store>();

        private static async Task<List<Store>> GetStoresAsync()
        {
            var client = new StadsAppRestApiClient();
            return await client.GetListAsync<Store>("Stores");
        }

        public async Task LoadDataAsync()
        {
            if (Stores.Count == 0) Stores.AddRange(await GetStoresAsync());
        }
    }
}