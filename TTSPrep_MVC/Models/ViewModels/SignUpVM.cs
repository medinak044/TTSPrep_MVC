using System.ComponentModel.DataAnnotations;

namespace TTSPrep_MVC.Models.ViewModels;

public class SignUpVM
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Must confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
