using System.ComponentModel.DataAnnotations;

namespace ShoesShelf.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter user name")]
        [Display(Name = "User name")]
        public string Login { get; set; } = "";

        [Required(ErrorMessage = "Enter password")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} and at most {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The passwords entered must match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
