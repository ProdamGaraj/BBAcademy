using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "No special symbols, or Russian letters allowed")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; }
    }
}
