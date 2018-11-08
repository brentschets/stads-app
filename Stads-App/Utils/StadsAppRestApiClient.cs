using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stads_App.Utils
{
    public class StadsAppRestApiClient : HttpClient
    {
        private static readonly Lazy<StadsAppRestApiClient> Lazy =
            new Lazy<StadsAppRestApiClient>(() => new StadsAppRestApiClient());

        public static StadsAppRestApiClient Instance => Lazy.Value;

        private StadsAppRestApiClient()
        {
        }

        private const string Host = "https://stadsapprestapi.azurewebsites.net/api/";

        public async Task<List<T>> GetListAsync<T>(string relUri)
        {
            return JsonConvert.DeserializeObject<List<T>>(await GetStringAsync(Host + relUri));
        }
    }
}
