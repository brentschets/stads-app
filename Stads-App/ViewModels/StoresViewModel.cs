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

        private IEnumerable<Store> _popular;

        public IEnumerable<Store> Popular
        {
            get => _popular ?? (_popular = GetPopular());
            set => _popular = value;
        }

        private static IEnumerable<Store> GetStores()
        {
            var client = new StadsAppRestApiClient();
            return client.GetList<Store>("stores").Result;
        }

        private static IEnumerable<Store> GetPopular()
        {
            var client = new StadsAppRestApiClient();
            return client.GetList<Store>("stores/popular").Result;
        }
    }
}