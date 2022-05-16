using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KimsWoodWorking.BusinessLogic.UserAccountManager;
using static KimsWoodWorking.BusinessLogic.OrderManager;
using KimsWoodWorking.Models;
using KimsWoodWorking.Models.ViewModels;

namespace KimsWoodWorking.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            //if the user is signed in
            if (GlobalVariables.currentUser.isSignedIn)
            {
                //if the user is an admin
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

        public ActionResult DeleteAccount() {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                return View();
            }
            else {
                return View("UnauthorizedAccess");
            }
        }

        public ActionResult ChangeUserPW()
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                return View();
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult ChangeUserRole()
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                return View();
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult EditOrder()
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                return View();
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult SearchOrder()
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                SearchOrderViewModel searchOrder = new SearchOrderViewModel();

                searchOrder.OrderSummaries = searchOrders(searchOrder.Order);

                return View(searchOrder);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchOrder(SearchOrderViewModel searchedOrder)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                SearchOrderViewModel viewModel = new SearchOrderViewModel();

                viewModel.Order = searchedOrder.Order;

                viewModel.OrderSummaries = searchOrders(searchedOrder.Order);

                return View(viewModel);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
    }
}