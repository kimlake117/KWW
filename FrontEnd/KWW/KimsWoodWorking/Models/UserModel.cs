using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class UserModel
    {
        public int user_id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        public Boolean isSignedIn { get; set; } = false;
    }
}