using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
