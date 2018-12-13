using System;

namespace RESTAPI.Exceptions
{
    public class EstablishmentException : Exception
    {
        public EstablishmentException(string message) : base(message)
        {
        }
    }
}