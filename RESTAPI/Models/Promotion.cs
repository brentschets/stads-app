namespace RESTAPI.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Store Store { get; set; }
    }
}
