using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login required")]
        [MaxLength(30, ErrorMessage = "Login is too big")]
        [MinLength(5, ErrorMessage = "Login is too short")]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter your password")]
        [MinLength(6, ErrorMessage = "Password is too short")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}
