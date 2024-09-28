using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.ViewModels.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "The field {0} is required."), StringLength(128)]
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "The field {0} is required."), StringLength(128)]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "The field {0} is required."), StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Required(ErrorMessage = "The field {0} is required."), StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}