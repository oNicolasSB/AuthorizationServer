using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.ViewModels.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "The field {0} is required."), StringLength(128)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "The field {0} is required."), StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "The field {0} is required.")]
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}