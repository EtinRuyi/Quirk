using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quirk.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage =
            "Password must be at least 6 alpha-numeric " +
            "and one special character")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
