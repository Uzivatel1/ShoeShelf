﻿using System.ComponentModel.DataAnnotations;

namespace ShoeShelf.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Enter user name")]
        [Display(Name = "User name")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me")]
        public bool IsPersistent { get; set; }
    }
}
