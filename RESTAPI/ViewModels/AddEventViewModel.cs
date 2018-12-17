using System.ComponentModel.DataAnnotations;

namespace RESTAPI.ViewModels
{
    public class AddEventViewModel
    {
        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        [Required] public int EstablishmentId { get; set; }
    }
}