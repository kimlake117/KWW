using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KimsWoodWorking.BusinessLogic.UserAccountManager;
using static KimsWoodWorking.BusinessLogic.OrderManager;
using static KimsWoodWorking.BusinessLogic.AdminManager;
using static KimsWoodWorking.BusinessLogic.ProductManager;
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
                if (userHasRole(GlobalVariables.currentUser, 2))
                {
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
        /*-------------------------------Delete account----------------------------------------*/
        public ActionResult DeleteAccount(ModifyAccountStatusViewModel vm)
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


        public ActionResult SelectUserDUA(int user_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                ViewBag.selectedUserName = getUserName(user_id);
                ViewBag.userID = user_id;

                return ConfirmDeleteAccount(user_id);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }

        public ActionResult ConfirmDeleteAccount(int user_id)
        {
            ViewBag.user_name = getUserName(user_id);
            ModifyAccountStatusViewModel vm = new ModifyAccountStatusViewModel { selectedUserID = user_id };

            return View("ConfirmDeleteAccount", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeleteAccount(ModifyAccountStatusViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                deleteUserAccount(vm.selectedUserID);
                ViewBag.message = "User: " + getUserName(vm.selectedUserID) + " was deactivated";
                return View("PostAdminAction");
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        /*-------------------------------reactivate account----------------------------------------*/
        public ActionResult ReactivateAccount(ModifyAccountStatusViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                vm.UserList = getDeactivatedUserList(vm.usernameSearchedFor);
                return View(vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult SelectUserRAUA(int user_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                ViewBag.selectedUserName = getUserName(user_id);
                ViewBag.userID = user_id;

                return ConfirmReactivateAccount(user_id);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult ConfirmReactivateAccount(int user_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                ViewBag.user_name = getUserName(user_id);
                ModifyAccountStatusViewModel vm = new ModifyAccountStatusViewModel { selectedUserID = user_id };

                return View("ConfirmReactivateAccount", vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmReactivateAccount(ModifyAccountStatusViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                reactivateUserAccount(vm.selectedUserID);
                ViewBag.message = "User: " + getUserName(vm.selectedUserID) + " was reactivated";
                return View("PostAdminAction");
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        /*-------------------------------Change user Password----------------------------------------*/
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
                ChangeUserPWViewModel vm = new ChangeUserPWViewModel
                {
                    selectedUserID = user_id
                };
                ViewBag.selectedUserName = getUserName(user_id);

                return View("ChangeUserPw", vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult ChangeUserPw()
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserPw(ChangeUserPWViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                changeUserPassword(vm);
                ViewBag.message = "Successfull password change for user: " + getUserName(vm.selectedUserID);
                return View("PostAdminAction");
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }

        /*-------------------------------edit Orders----------------------------------------*/
        public ActionResult EditOrder(int order_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                EditOrderViewModel vm = new EditOrderViewModel
                {
                    productsList = getOrderDetails(order_id)
                };
                return View();
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        /*-------------------------------Search for order----------------------------------------*/
        public ActionResult ClearSearchOrders()
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                SearchOrderViewModel searchOrder = new SearchOrderViewModel();

                searchOrder.OrderSummaries = searchOrders(searchOrder.Order);

                return View("SearchOrder", searchOrder);
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
                SearchOrderViewModel viewModel = new SearchOrderViewModel
                {
                    Order = searchedOrder.Order,

                    OrderSummaries = searchOrders(searchedOrder.Order)
                };

                return View(viewModel);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        /*-------------------------------Edit Product----------------------------------------*/
        public ActionResult EditProduct(EditProductViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                vm.ProductsList = GetALLProductList(vm.ProductSearchedFor);

                return View(vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        public ActionResult SelectProduct(int product_id)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {

                EditProductViewModel vm = new EditProductViewModel();
                vm.SelectedProduct = GetProductByID(product_id);
                vm.editedProduct.product_id = product_id;
                return View(vm);
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectProduct(EditProductViewModel vm)
        {
            if (userHasRole(GlobalVariables.currentUser, 2))
            {
                int returnResult = updateProduct(vm);

                if (returnResult == 0)
                {
                    ViewBag.message = "No changes made to product: "+getProductName(vm.editedProduct.product_id);
                    return View("PostAdminAction");
                }
                if (returnResult == 1)
                {
                    ViewBag.message = "Success, changes made to product: " + getProductName(vm.editedProduct.product_id);
                    return View("PostAdminAction");
                }
                else {
                    ViewBag.message = "Error, no changes made to product: " + getProductName(vm.editedProduct.product_id);
                    return View("PostAdminAction");
                }
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }
    }
}
   