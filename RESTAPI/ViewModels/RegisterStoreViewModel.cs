using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RESTAPI.ViewModels
{
    public class RegisterStoreViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }

        [Required]
        public string StoreName { get; set; }

        [Required]
        public string StoreDescription { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public IFormFile Image { get;set; }
    }
}