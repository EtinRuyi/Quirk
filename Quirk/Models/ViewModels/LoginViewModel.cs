using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Quirk.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage =
            "Password must be at least 6 alpha-numeric " +
            "and one special character")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
