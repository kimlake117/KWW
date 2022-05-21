using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using KimsWoodWorking.Models.databaseModels;

namespace KimsWoodWorking.Models.ViewModels
{
    public class ChangeUserPWViewModel
    {
        public List<UserDBModel> UserList { get; set; } = new List<UserDBModel>();

        [Display(Name = "User Name")]
        public string usernameSearchedFor { get; set; } = string.Empty;

        [Display(Name = "Selected User")]
        public int selectedUserID { get; set; } = -1;

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Passwords must match.")]
        public string comfirmPassword { get; set; }

    }
}