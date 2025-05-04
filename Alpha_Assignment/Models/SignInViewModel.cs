using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class SignInViewModel
{
    [Required]
    [Display(Name = "Email", Prompt = "Enter email address")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = "Enter password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$",
            ErrorMessage = "Password must be at least 12 characters long, with at least one uppercase letter, one lowercase letter, one number and one special character.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
