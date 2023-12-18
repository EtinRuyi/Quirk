using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quirk.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
