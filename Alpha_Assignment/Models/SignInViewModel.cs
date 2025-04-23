using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class SignInViewModel
{
    [Required]
    [Display(Name = "Password", Prompt = "Enter password")]
    [RegularExpression(@"")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Confirm Passoword", Prompt = "Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    [DataType(DataType.Password)]

    public bool IsPersistant { get; set; }
}
