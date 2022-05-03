using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;

namespace KimsWoodWorking
{
    public static class GlobalVariables
    {
        public static int CurrentUser_id { get; set; }

        public static string CurrentUser_Name { get; set; } = "Account";

        public static string CurrentUser_Email { get; set; }

        public static bool isSignedIn { get; set; } = false;

        public static string logInOut { get; set; } = "Log In";

        public static string attemptedAccessURL { get; set; } = "~/Home/Index";

    }
}