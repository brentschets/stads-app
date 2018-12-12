using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class AuthenticateUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
