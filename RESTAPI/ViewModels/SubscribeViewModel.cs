using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class SubscribeViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int EstablishmentId { get; set; }
    }
}
