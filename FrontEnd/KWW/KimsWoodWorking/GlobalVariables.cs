using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;

namespace KimsWoodWorking
{
    public static class GlobalVariables
    {
        public static UserModel currentUser { get; set; } = new UserModel();

        public static string logInOut { get; set; } = "Log In";

        public static string attemptedAccessURL { get; set; } = "~/Home/Index";

    }
}