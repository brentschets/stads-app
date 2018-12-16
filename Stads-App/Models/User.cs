using System;
using System.Collections.Generic;

namespace Stads_App.Models
{
    public class User : ICloneable
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public IList<int> Subscriptions { internal get; set; }
        public int? StoreId { get; set; }

        public object Clone()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Password = Password,
                UserId = UserId,
                Token = Token,
                Subscriptions = new List<int>(Subscriptions),
                StoreId = StoreId
            };
        }
        
        public bool EqualsForUpdate(User user)
        {
            return user.FirstName == FirstName && user.LastName == LastName && user.Username == Username;
        }
    }
}