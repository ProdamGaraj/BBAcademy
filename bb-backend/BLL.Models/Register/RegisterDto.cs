namespace BLL.Models.Register;

public class RegisterDto
{
    public string Surname { get; set; }
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }
    public string Login { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}