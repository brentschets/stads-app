using System.Collections.Generic;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public class StoresViewModel
    {
        private IEnumerable<Store> _stores;

        public IEnumerable<Store> Stores
        {
            get => _stores ?? (_stores = GetStores());
            set => _stores = value;
        }

        private IEnumerable<Store> _mostVisited;

        public IEnumerable<Store> MostVisited
        {
            get => _mostVisited ?? (_mostVisited = GetPopular());
            set => _mostVisited = value;
        }

        private static IEnumerable<Store> GetStores()
        {
            var client = new StadsAppRestApiClient();
            return client.GetList<Store>("stores");
        }

        private static IEnumerable<Store> GetPopular()
        {
            var client = new StadsAppRestApiClient();
            return client.GetList<Store>("stores/mostvisited");
        }
    }
}