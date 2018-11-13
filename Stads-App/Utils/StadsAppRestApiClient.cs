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

        //local
        /*private StadsAppRestApiClient() : base(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        })
        {
        }*/

        //deploy
        private StadsAppRestApiClient()
        {
        }

        //deploy
        private const string Host = "https://stadsapprestapi.azurewebsites.net/api/";
        //local
        //private const string Host = "https://localhost:44301/api/";

        public async Task<List<T>> GetListAsync<T>(string relUri)
        {
            return JsonConvert.DeserializeObject<List<T>>(await GetStringAsync(Host + relUri));
        }
    }
}