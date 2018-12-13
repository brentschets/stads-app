using System;

namespace RESTAPI.Exceptions
{
    public class EventException : Exception
    {
        public EventException(string message) : base(message)
        {
        }
    }
}