using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "First Name", Prompt = "Enter first name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name", Prompt = "Enter last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = null!;

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

        [Required]
        [Display(Name = "Confirm Password", Prompt = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Range(typeof(bool), "true", "true")]
        public bool TermsAndConditions { get; set; }
    }
}
