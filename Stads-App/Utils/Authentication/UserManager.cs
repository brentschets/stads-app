using System;
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

        public static User CurrentUser
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

        public static bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public static bool IsLoggedIn(User user)
        {
            if (user == null) throw new ArgumentException("User cannot be null", nameof(user));

            return CurrentUser?.UserId == user.UserId;
        }

        public AuthenticationResult Authenticate(string username, string password)
        {
            if (IsLoggedIn()) throw new InvalidOperationException("Another user is already logged in");

            var result = StadsAppRestApiClient.Instance.AuthenticateUser(username, password);

            if (result.Success) CurrentUser = result.User;

            return result;
        }

        public void Logout()
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");

            CurrentUser = null;
        }

        public AuthenticationResult Register(string username, string password, string firstName, string lastName)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            return StadsAppRestApiClient.Instance.RegisterUser(user);
        }

        public AuthenticationResult Update(User user)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");
            if (!IsLoggedIn(user))
                throw new InvalidOperationException("The logged in user's id does not match the provided users's id");

            var result = StadsAppRestApiClient.Instance.UpdateUser(user);
            if (result.Success)
            {
                CurrentUser = user;
            }

            return result;
        }

        public void Delete(User user)
        {
            if (!IsLoggedIn(user)) throw new InvalidOperationException("The deleted user must be logged in");
            StadsAppRestApiClient.Instance.DeleteUser(user.UserId);
        }
    }
}