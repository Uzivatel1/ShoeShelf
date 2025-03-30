using System.ComponentModel.DataAnnotations;

namespace ShoeShelf.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter user name")]
        [Display(Name = "User name")]
        public string Login { get; set; } = "";

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
