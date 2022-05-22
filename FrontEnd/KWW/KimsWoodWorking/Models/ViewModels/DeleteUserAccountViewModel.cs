using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models.ViewModels
{
    public class DeleteUserAccountViewModel
    {
        public List<UserDBModel> UserList { get; set; } = new List<UserDBModel>();

        [Display(Name = "User Name")]
        public string usernameSearchedFor { get; set; } = string.Empty;

        [Display(Name = "Selected User")]
        public int selectedUserID { get; set; } = -1;
    }
}