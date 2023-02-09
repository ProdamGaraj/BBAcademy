using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name required")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "This field must contain only Russian and English letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname required")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "This field must contain only Russian and English letters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "This field must contain only Russian and English letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"^((([0-9A-Za-z]{1}[-0-9A-z.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}.){1,2}[-A-Za-z]{2,})$", ErrorMessage = "This Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Login required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "This field must contain only English letters and numbers.")]
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
