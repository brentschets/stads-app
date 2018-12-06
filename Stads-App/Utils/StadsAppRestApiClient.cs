﻿using System;
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

        public AuthenticationResult AuthenticateUser(string username, string password)
        {
            var task = PostAsync($"{Host}Users/Authenticate",
                PrepareContent(new {Username = username, Password = password}));
            task.Wait();
            return ProcessResponse(task.Result);
        }

        public AuthenticationResult RegisterUser(User user)
        {
            var task = PostAsync($"{Host}Users/Register", PrepareContent(user));
            task.Wait();
            return ProcessResponse(task.Result);
        }

        public AuthenticationResult UpdateUser(User user)
        {
            var task = PostAsync($"{Host}Users/Update/{user.UserId}", PrepareContent(user));
            task.Wait();
            return ProcessResponse(task.Result);
        }

        public void DeleteUser(int userId)
        {
            var task = DeleteAsync($"{Host}Users/Delete/{userId}");
            task.Wait();
        }

        public AuthenticationResult Subscribe(int userId, int establishmentId)
        {
            var task = PostAsync($"{Host}Users/Subscribe/{userId}", PrepareContent(establishmentId));
            task.Wait();
            return ProcessResponse(task.Result);
        }

        public AuthenticationResult Unsubscribe(int userId, int establishmentId)
        {
            var task = PostAsync($"{Host}Users/Unsubsribe/{userId}", PrepareContent(establishmentId));
            task.Wait();
            return ProcessResponse(task.Result);
        }

        #region Helpers

        private HttpContent PrepareContent(object o)
        {
            var content = new StringContent(JsonConvert.SerializeObject(o));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            DefaultRequestHeaders.Authorization = UserManager.IsLoggedIn()
                ? new AuthenticationHeaderValue("Bearer", UserManager.CurrentUser.Token)
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