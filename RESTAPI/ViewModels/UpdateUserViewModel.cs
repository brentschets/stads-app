using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class UpdateUserViewModel
    {
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
