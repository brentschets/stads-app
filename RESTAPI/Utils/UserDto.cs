﻿using System.Collections.Generic;
using RESTAPI.Models;

namespace RESTAPI.Utils
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get;set; }
        public string LastName { get; set; }
        public string Username { get;set; }
        public string Password { get; set; }
        public IEnumerable<UserEstablishment> Subscriptions { get; set; }
    }
}