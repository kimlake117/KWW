using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class NewUserModel
    {
        [Required(ErrorMessage ="User name is required.")]
        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =8,ErrorMessage ="Password must be between 8 and 100 charecters.")]
        public String Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords must match.")]
        public  String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "Emails must match.")]
        public String ConfirmEmail { get; set; }

    }
}