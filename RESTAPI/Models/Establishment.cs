using System.Collections.Generic;

namespace RESTAPI.Models
{
    public class Establishment
    {
        public int EstablishmentId { get; set; }
        public string ImgPath { get; set; }
        public int Visited { get; set; }
        public Address Address { get; set; }
        public Store Store { get; set; }
        public IEnumerable<UserEstablishment> SubscribedUsers { get; set; }
    }
}