﻿using System.Collections.Generic;

namespace RESTAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IList<UserEstablishment> Subscriptions { get; set; }
        public int? StoreId { get; set; }
    }
}