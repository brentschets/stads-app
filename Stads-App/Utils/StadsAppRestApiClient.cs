using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private readonly UserManager _userManager;

        //local
        private StadsAppRestApiClient() : base(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        })
        {
            _userManager = new UserManager();
        }

        //deploy
        //private StadsAppRestApiClient()
        //{
        //  _userManager = new UserManager();
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
            return ProcessResponse(await PostAsync($"{Host}Users/Authenticate",
                PrepareContent(new {username, password})));
        }

        public async Task<AuthenticationResult> RegisterUserAsync(User user)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Register", PrepareContent(user)));
        }

        public async Task<AuthenticationResult> UpdateUserAsync(User user)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Update/{user.UserId}", PrepareContent(user)));
        }

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync($"{Host}Users/Delete/{userId}");
        }

        public async Task<AuthenticationResult> SubscribeAsync(int userId, int establishmentId)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Subscribe",
                PrepareContent(new {userId, establishmentId})));
        }

        public async Task<AuthenticationResult> UnsubscribeAsync(int userId, int establishmentId)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Unsubscribe",
                PrepareContent(new {userId, establishmentId})));
        }

        #region Helpers

        private HttpContent PrepareContent(object o)
        {
            var content = new StringContent(JsonConvert.SerializeObject(o));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            DefaultRequestHeaders.Authorization = _userManager.IsLoggedIn()
                ? new AuthenticationHeaderValue("Bearer", _userManager.CurrentUser.Token)
                : null;
            return content;
        }

        private static AuthenticationResult ProcessResponse(HttpResponseMessage response)
        {
            var result = new AuthenticationResult();

            if (response.IsSuccessStatusCode)
            {
                result.Success = true;
                var task = response.Content.ReadAsStringAsync();
                task.Wait();
                result.User = JsonConvert.DeserializeObject<User>(task.Result);
            }
            else
            {
                result.Success = false;
                var task = response.Content.ReadAsStringAsync();
                task.Wait();
                result.Error = JsonConvert.DeserializeObject<AuthenticationError>(task.Result);
            }

            return result;
        }

        #endregion
    }
}