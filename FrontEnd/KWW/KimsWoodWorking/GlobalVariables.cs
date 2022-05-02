using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;

namespace KimsWoodWorking
{
    public static class GlobalVariables
    {
        public static UserModel CurrentUser { get; set; }

        public static string logInOut { get; set; } = "log In";

        public static string userName { get; set; } = "Account";

    }
}