using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class AddPromotionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int StoreId { get; set; }
    }
}
