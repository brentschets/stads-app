namespace RESTAPI.Models
{
    public class UserEstablishment
    {
        public int? UserId { get; set; }
        public int? EstablishmentId { get; set; }
        public User User { get; set; }
        public Establishment Establishment { get; set; }
    }
}
