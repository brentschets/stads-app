using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class AddEstablishmentViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Street {get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string FileName { get; set; }
    }
}
