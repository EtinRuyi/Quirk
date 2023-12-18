using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Quirk.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
