using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models.databaseModels
{
    public class UserDBModel
    {
        public int user_id { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }
    }
}