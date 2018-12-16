using System;
using System.Threading.Tasks;
using Windows.Storage;
using Stads_App.Models;

namespace Stads_App.Utils.Authentication
{
    public class UserManager
    {
        public delegate void ChangeEvent(User user);

        public static event ChangeEvent AccountChanged;

        public delegate void UserUpdateEvent(User user);

        public static event UserUpdateEvent UserUpdated;

        private static User _currentUser;

        public User CurrentUser
        {
            get => _currentUser?.Clone() as User;
            private set
            {
                var prev = _currentUser;
                _currentUser = value;
                if (prev?.UserId == value?.UserId) UserUpdated?.Invoke(CurrentUser);
                else AccountChanged?.Invoke(CurrentUser);
            }
        }

        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }

        public bool IsEntrepreneur()
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is logged in");

            return _currentUser.StoreId != null;
        }

        public bool IsLoggedIn(User user)
        {
            if (user == null) throw new ArgumentException("User cannot be null", nameof(user));

            return _currentUser?.UserId == user.UserId;
        }

        public bool IsLoggedIn(int userId)
        {
            return _currentUser?.UserId == userId;
        }

        public async Task<RestApiResponse> AuthenticateAsync(string username, string password)
        {
            if (IsLoggedIn()) throw new InvalidOperationException("Another user is already logged in");

            var result = await StadsAppRestApiClient.Instance.AuthenticateUserAsync(username, password);

            if (result.Success) CurrentUser = result.User;

            return result;
        }

        public void Logout()
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");

            CurrentUser = null;
        }

        public async Task<RestApiResponse> RegisterAsync(string username, string password, string firstName,
            string lastName)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            return await StadsAppRestApiClient.Instance.RegisterUserAsync(user);
        }

        public async Task<RestApiResponse> UpdateAsync(User user)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");
            if (!IsLoggedIn(user))
                throw new InvalidOperationException("The logged in user's id does not match the provided users's id");

            var result = await StadsAppRestApiClient.Instance.UpdateUserAsync(user);
            if (result.Success)
            {
                CurrentUser = user;
            }

            return result;
        }

        public async void DeleteAsync(int userId)
        {
            if (!IsLoggedIn(userId)) throw new InvalidOperationException("The deleted user must be logged in");
            await StadsAppRestApiClient.Instance.DeleteUserAsync(userId);
            Logout();
        }

        public async Task<RestApiResponse> SubscribeAsync(int establishmentId)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");

            var result = await StadsAppRestApiClient.Instance.SubscribeAsync(_currentUser.UserId, establishmentId);

            if (result.Success) _currentUser.Subscriptions.Add(establishmentId);

            return result;
        }

        public async Task<RestApiResponse> UnsubscribeAsync(int establishmentId)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");

            var result = await StadsAppRestApiClient.Instance.UnsubscribeAsync(_currentUser.UserId, establishmentId);

            if (result.Success) _currentUser.Subscriptions.Remove(establishmentId);

            return result;
        }

        public bool IsSubscribed(int establishmentId)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");

            return _currentUser.Subscriptions.Contains(establishmentId);
        }

        public async Task<RestApiResponse> UpdateStoreAsync(Store store)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");
            if (_currentUser.StoreId != store.StoreId)
                throw new InvalidOperationException(
                    "The user to which this store belongs is not the user that is logged in");

            return await StadsAppRestApiClient.Instance.UpdateStoreAsync(store);
        }

        public async Task AddEstablishmentAsync(Establishment establishment, StorageFile image)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");
            if (_currentUser.StoreId != establishment.Store.StoreId)
                throw new InvalidOperationException(
                    "The user to which this store belongs is not the user that is logged in");

            await StadsAppRestApiClient.Instance.AddEstablishmentAsync(establishment, image);
        }

        public async Task UpdateEstablishmentAsync(Establishment establishment)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");
            if (_currentUser.StoreId != establishment.Store.StoreId)
                throw new InvalidOperationException(
                    "The user to which this store belongs is not the user that is logged in");

            await StadsAppRestApiClient.Instance.UpdateEstablishmentAsync(establishment);
        }
    }
}