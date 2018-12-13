using System;

namespace RESTAPI.Exceptions
{
    public class StoreException : Exception
    {
        public StoreException(string message) : base(message)
        {
        }
    }
}