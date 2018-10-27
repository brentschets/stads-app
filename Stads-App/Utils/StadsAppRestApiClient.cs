using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stads_App.Utils
{
    public class StadsAppRestApiClient : HttpClient
    {
        private const string Host = "https://stadsapprestapi.azurewebsites.net/api/";

        public async Task<List<T>> GetList<T>(string relUri)
        {
            var res = await GetStringAsync(Host + relUri);
            var lst = JsonConvert.DeserializeObject<List<T>>(res);
            return lst;
        }
    }
}
