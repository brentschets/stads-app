using System;
using Stads_App.Models;

namespace Stads_App.Utils.Authentication
{
    public class UserManager
    {
        public static User CurrentUser { get; private set; }

        public static bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public static bool IsLoggedIn(User user)
        {
            if (user == null) throw new ArgumentException("User cannot be null", nameof(user));

            return CurrentUser.UserId == user.UserId;
        }

        public AuthenticationResult Authenticate(string username, string password)
        {
            if (IsLoggedIn()) throw new InvalidOperationException("Another user is already logged in");

            var task = StadsAppRestApiClient.Instance.AuthenticateUserAsync(username, password);
            task.Wait();
            var result = task.Result;

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

            var task = StadsAppRestApiClient.Instance.RegisterUserAsync(user);
            task.Wait();

            return task.Result;
        }

        public AuthenticationResult Update(User user)
        {
            if (!IsLoggedIn()) throw new InvalidOperationException("No user is currently logged in");
            if (CurrentUser.UserId != user.UserId)
                throw new InvalidOperationException("The logged in user's id does not match the provided users's id");

            var task = StadsAppRestApiClient.Instance.UpdateUserAsync(user);
            task.Wait();

            return task.Result;
        }

        public void Delete(User user)
        {
            if (!IsLoggedIn(user)) throw new InvalidOperationException("The deleted user must be logged in");
            var task = StadsAppRestApiClient.Instance.DeleteUserAsync(user.UserId);
            task.Wait();
        }
    }
}