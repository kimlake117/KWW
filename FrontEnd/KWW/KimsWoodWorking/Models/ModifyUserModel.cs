using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class ModifyUserModel
    {

        [Required(ErrorMessage = "New password is required.")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 charecters.")]
        public String Password { get; set; }

        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "New email is required.")]
        [Display(Name = "New Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Display(Name = "Confirm New Email")]
        [Compare("Email", ErrorMessage = "Emails must match.")]
        public String ConfirmEmail { get; set; }

    }
}