using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KimsWoodWorking.BusinessLogic.UserAccount;

namespace KimsWoodWorking.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                if (userHasRole(GlobalVariables.currentUser,2)) {
                    return View();
                }
                return View("UnauthorizedAccess");
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Admin/";
                return Redirect("~/Account/LogIn");
            }
        }
    }
}