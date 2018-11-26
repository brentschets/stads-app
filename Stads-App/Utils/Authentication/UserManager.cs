using System;
using Stads_App.Models;

namespace Stads_App.Utils.Authentication
{
    public class UserManager
    {
        public delegate void ChangeEvent(User user);

        public static event ChangeEvent AccountChanged;

        private static User _currentUser;

        public static User CurrentUser
        {
            get => _currentUser?.Clone() as User;
            private set
            {
                _currentUser = value;
                AccountChanged?.Invoke(value);
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
            if (CurrentUser.UserId != user.UserId)
                throw new InvalidOperationException("The logged in user's id does not match the provided users's id");

            return StadsAppRestApiClient.Instance.UpdateUser(user);
        }

        public void Delete(User user)
        {
            if (!IsLoggedIn(user)) throw new InvalidOperationException("The deleted user must be logged in");
            StadsAppRestApiClient.Instance.DeleteUser(user.UserId);
        }
    }
}