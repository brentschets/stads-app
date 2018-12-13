using System;

namespace RESTAPI.Exceptions
{
    public class PromotionException : Exception
    {
        public PromotionException(string message) : base(message)
        {
        }
    }
}