using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KimsWoodWorking.BusinessLogic.UserAccountManager;
using static KimsWoodWorking.BusinessLogic.OrderManager;
using static KimsWoodWorking.BusinessLogic.AdminManager;
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

        public ActionResult SearchForUser()
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                ChangeUserPWViewModel vm = new ChangeUserPWViewModel();
                vm.UserList = getUserList(vm.usernameSearchedFor);
                return View(vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchForUser(ChangeUserPWViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                vm.UserList = getUserList(vm.usernameSearchedFor);
                return View(vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult SelectUser(int user_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                ChangeUserPWViewModel vm = new ChangeUserPWViewModel();

                vm.selectedUserID = user_id;
                ViewBag.selectedUserName = getUserName(user_id);

                return View("ChangeUserPw",vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult ChangeUserPw() {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                return View();
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserPw(ChangeUserPWViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                changeUserPassword(vm);
                ViewBag.message = "Successfull password change for user: "+ getUserName(vm.selectedUserID);
                return View("PostAdminAction");
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult editProduct()
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
        public ActionResult EditOrder(int order_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                EditOrderViewModel vm = new EditOrderViewModel();
                vm.productsList = getOrderDetails(order_id);
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