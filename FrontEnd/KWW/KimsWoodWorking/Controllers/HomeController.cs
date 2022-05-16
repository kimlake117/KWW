using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;
using static KimsWoodWorking.BusinessLogic.UserAccount;

namespace KimsWoodWorking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            if (!GlobalVariables.currentUser.isSignedIn) {
                GlobalVariables.attemptedAccessURL = "~/Home/Index";
            }

            return View();
        }

        public ActionResult NavBar() { 
            NavBarViewModel navBarViewModel = new NavBarViewModel();

            //if the user has admin roll
            if (userHasRole( GlobalVariables.currentUser,2)) { 
                navBarViewModel.isAdmin = true;
            }
            //if the user has sitefocal role
            if (userHasRole(GlobalVariables.currentUser,3))
                { 
                navBarViewModel.isSiteFocal = true;
            }
            return PartialView(navBarViewModel);
        }
    }
}
