using System.Collections.Generic;

namespace Stads_App.Models
{
    public class Establishment
    {
        public int EstablishmentId { get; set; }
        public string ImgPath { get; set; }
        public int Visited { get; set; }
        public Address Address { get; set; }
        public Store Store { get; set; }
        public IEnumerable<User> SubscribedUsers { get; set; }

        public bool EqualsForUpdate(Establishment establishment)
        {
            return EstablishmentId == establishment.EstablishmentId && Address.Street == establishment.Address.Street &&
                   Address.Number == establishment.Address.Number;
        }
    }
}