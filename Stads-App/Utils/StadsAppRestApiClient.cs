using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stads_App.Models;
using Stads_App.Utils.Authentication;

namespace Stads_App.Utils
{
    public class StadsAppRestApiClient : HttpClient
    {
        private static readonly Lazy<StadsAppRestApiClient> Lazy =
            new Lazy<StadsAppRestApiClient>(() => new StadsAppRestApiClient());

        public static StadsAppRestApiClient Instance => Lazy.Value;

        //local
        private StadsAppRestApiClient() : base(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        })
        {
        }

        //deploy
        //private StadsAppRestApiClient()
        //{
        //}

        //deploy
        //private const string Host = "https://stadsapprestapi.azurewebsites.net/api/";
        //local
        private const string Host = "https://localhost:44301/api/";

        public async Task<List<T>> GetListAsync<T>(string relUri)
        {
            return JsonConvert.DeserializeObject<List<T>>(await GetStringAsync(Host + relUri));
        }

        public async Task<AuthenticationResult> AuthenticateUserAsync(string username, string password)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new {username, password}));
            var response = await PostAsync($"{Host}/Users/Authenticate", content);

            return ProcessResponse(response);
        }

        public async Task<AuthenticationResult> RegisterUserAsync(User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user));
            var response = await PostAsync($"{Host}/Users/Register", content);

            return ProcessResponse(response);
        }

        public async Task<AuthenticationResult> UpdateUserAsync(User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user));
            var response = await PostAsync($"{Host}/Users/Update/{user.UserId}", content);

            return ProcessResponse(response);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync($"{Host}/Users/Delete/{userId}");
        }

        #region Helpers

        private static AuthenticationResult ProcessResponse(HttpResponseMessage response)
        {
            var result = new AuthenticationResult();

            if (response.IsSuccessStatusCode)
            {
                result.Success = true;
                result.User = JsonConvert.DeserializeObject<User>(response.Content.ToString());
            }
            else
            {
                result.Success = false;
                result.Error = JsonConvert.DeserializeObject<AuthenticationError>(response.Content.ToString());
            }

            return result;
        }

        #endregion
    }
}