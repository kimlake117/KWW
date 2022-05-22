using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models.databaseModels
{
    public class UserDBModel
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }
        [Display(Name = "User Name")]
        public String user_name { get; set; }
        [Display(Name = "Password")]
        public String password { get; set; }
        [Display(Name = "Email")]
        public String email { get; set; }

        public int active { get; set; } = -1;
    }
}