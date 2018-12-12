using System;

namespace RESTAPI.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {
        }
    }
}