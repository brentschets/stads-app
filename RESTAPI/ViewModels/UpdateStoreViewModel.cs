using System.ComponentModel.DataAnnotations;
using RESTAPI.Models;

namespace RESTAPI.ViewModels
{
    public class UpdateStoreViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Category Category { get; set; }
    }
}