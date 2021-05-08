using System.ComponentModel.DataAnnotations;

namespace Esourcing.UI.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Passord min 4 must be character")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
