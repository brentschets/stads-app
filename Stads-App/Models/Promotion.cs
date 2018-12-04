namespace Stads_App.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public int Visited { get; set; }
        public Store Store { get; set; }
    }
}
