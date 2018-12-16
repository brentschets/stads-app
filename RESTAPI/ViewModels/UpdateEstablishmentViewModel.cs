using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class UpdateEstablishmentViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public int EstablishmentId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Number { get; set; }
    }
}
