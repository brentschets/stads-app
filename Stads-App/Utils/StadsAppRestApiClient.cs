using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Stads_App.Models;
using Stads_App.Utils.Authentication;

namespace Stads_App.Utils
{
    public class StadsAppRestApiClient : HttpClient
    {
        public delegate void UpdateEvent();

        public event UpdateEvent Updated;

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
        //    _userManager = new UserManager();
        //}

        //deploy
        //private const string Host = "https://stadsapprestapi.azurewebsites.net/api/";
        //local
        private const string Host = "https://localhost:44301/api/";

        public async Task<List<T>> GetListAsync<T>(string relUri)
        {
            return JsonConvert.DeserializeObject<List<T>>(await GetStringAsync(Host + relUri));
        }

        public async Task<T> GetSingleObjectAsync<T>(string relUri)
        {
            return JsonConvert.DeserializeObject<T>(await GetStringAsync(Host + relUri));
        }

        public async Task<RestApiResponse> AuthenticateUserAsync(string username, string password)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Authenticate",
                PrepareContent(new {username, password})));
        }

        public async Task<RestApiResponse> RegisterUserAsync(User user)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Register", PrepareContent(user)));
        }

        public async Task<RestApiResponse> UpdateUserAsync(User user)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Update/{user.UserId}", PrepareContent(user)));
        }

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync($"{Host}Users/Delete/{userId}");
        }

        public async Task<RestApiResponse> SubscribeAsync(int userId, int establishmentId)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Subscribe",
                PrepareContent(new {userId, establishmentId})));
        }

        public async Task<RestApiResponse> UnsubscribeAsync(int userId, int establishmentId)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/Unsubscribe",
                PrepareContent(new {userId, establishmentId})));
        }

        public async Task<RestApiResponse> RegisterStoreAsync(Store store, StorageFile image, User user)
        {
            byte[] bytes;
            using (var stream = await image.OpenStreamForReadAsync())
            {
                var binaryReader = new BinaryReader(stream);
                bytes = binaryReader.ReadBytes((int) stream.Length);
            }

            var res =  ProcessResponse(await PostAsync($"{Host}Users/RegisterStore", PrepareContent(new
            {
                // add store
                StoreName = store.Name,
                StoreDescription = store.Description,
                store.Category.CategoryId,
                Image = Convert.ToBase64String(bytes),
                FileName = image.Name,

                // add user
                user.FirstName,
                user.LastName,
                user.Username,
                user.Password
            })));

            if (res.Success) Updated?.Invoke();
            return res;
        }

        public async Task<RestApiResponse> UpdateStoreAsync(Store store)
        {
            return ProcessResponse(await PostAsync($"{Host}Users/UpdateStore/{store.StoreId}", PrepareContent(store)));
        }

        public async Task AddEstablishmentAsync(Establishment establishment, StorageFile image)
        {
            byte[] bytes;
            using (var stream = await image.OpenStreamForReadAsync())
            {
                var binaryReader = new BinaryReader(stream);
                bytes = binaryReader.ReadBytes((int) stream.Length);
            }

            await PostAsync($"{Host}Establishments", PrepareContent(new
            {
                // add establishment
                establishment.Address.Street,
                establishment.Address.Number,
                establishment.Store.StoreId,

                // add image
                Image = Convert.ToBase64String(bytes),
                FileName = image.Name
            }));
        }

        public async Task UpdateEstablishmentAsync(Establishment establishment)
        {
            await PostAsync($"{Host}Establishments/Update", PrepareContent(new
            {
                establishment.EstablishmentId,
                establishment.Address.Street,
                establishment.Address.Number
            }));
        }

        // not async because task is awaited indefinitely for some reason thanks obama :(
        public void DeleteEstablishmentAsync(int establishmentId)
        {
            var task = DeleteAsync($"{Host}Establishments/{establishmentId}");
            task.Wait();
        }

        public async Task DeletepromotionAsync(int promotionId)
        {
            await DeleteAsync($"{Host}promotions/{promotionId}");
            Updated?.Invoke();
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            var res = await PostAsync($"{Host}promotions", PrepareContent(new
            {
                promotion.Name,
                promotion.Store.StoreId
            }));

            var dbPromotion = JsonConvert.DeserializeObject<Promotion>(await res.Content.ReadAsStringAsync());
            promotion.PromotionId = dbPromotion.PromotionId;

            Updated?.Invoke();
        }

        public async Task DeleteEventAsync(int eventId)
        {
            await DeleteAsync($"{Host}Events/{eventId}");
            Updated?.Invoke();
        }

        public async Task AddEventAsync(Event @event)
        {
            var res = await PostAsync($"{Host}Events", PrepareContent(new
            {
                @event.Name,
                @event.Description,
                @event.Establishment.EstablishmentId
            }));

            var dbEvent = JsonConvert.DeserializeObject<Event>(await res.Content.ReadAsStringAsync());
            @event.EventId = dbEvent.EventId;

            Updated?.Invoke();
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

        private static RestApiResponse ProcessResponse(HttpResponseMessage response)
        {
            var result = new RestApiResponse();

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
                result.Error = JsonConvert.DeserializeObject<RestApiError>(task.Result);
            }

            return result;
        }

        #endregion
    }
}