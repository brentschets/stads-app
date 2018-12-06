using System.Collections.Generic;

namespace Stads_App.Models
{
    public class Establishment
    {
        public int EstablishmentId { get; set; }
        public string ImgPath { get; set; }
        public int Visited { get; set; }
        public Address Address { get; set; }
        public Store Store { get;set; }
        public IEnumerable<UserEstablishment> SubscribedUsers { get; set; }
    }
}
