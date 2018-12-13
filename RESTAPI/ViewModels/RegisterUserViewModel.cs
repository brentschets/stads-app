using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
