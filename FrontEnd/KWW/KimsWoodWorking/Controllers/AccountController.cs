using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;
using KimsWoodWorking.BusinessLogic;
using static KimsWoodWorking.BusinessLogic.UserAccountManager;
using static KimsWoodWorking.BusinessLogic.OrderManager;



namespace KimsWoodWorking.Controllers
{
    public class AccountController : Controller
    {

//**********************Login and sign up**********************************************
        public ActionResult Index()
        {
            return View("LogIn");
        }

        //this handles both loging in and loggin out
        public ActionResult LogIn()
        {
            if (GlobalVariables.currentUser.isSignedIn) {

                //reset global variables
                GlobalVariables.currentUser = new UserModel();
                GlobalVariables.logInOut = "Log In";


                return View("~/Views/Home/Index.cshtml");
            }
            //if they where not logged in, return the login form
            else {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserModel user)
        {
            if (ModelState.IsValid) {
                if (UserAccountManager.pwMatch(user)) {

                    GlobalVariables.logInOut = "Log Out";
                    GlobalVariables.currentUser.isSignedIn = true;
                    GlobalVariables.currentUser.Email = user.Email;
                    GlobalVariables.currentUser.UserName = user.UserName;
                    GlobalVariables.currentUser.user_id = UserAccountManager.getUserId(user);
                    GlobalVariables.currentUser.roleList = getUserRoles();

                    string redirectURL = GlobalVariables.attemptedAccessURL;

                    GlobalVariables.attemptedAccessURL = "~/Home/Index";

                    return Redirect(redirectURL);
                }
            }
            return View("invalidLogIn");
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(NewUserModel newUser)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateNewUser(newUser.UserName, newUser.Email, newUser.Password);
            }
            UserModel user = new UserModel();

            user.UserName = newUser.UserName;
            user.Email = newUser.Email;
            user.Password = newUser.Password;

            LogIn(user);

            return View("~/Views/Home/Index.cshtml");
        }
        //**********************Cart and Orders********************************
        public ActionResult ViewCart()
        {
            if (GlobalVariables.currentUser.isSignedIn)
            {

                List<UserCartItemModel> userCart = UserCartManager.getUserCart();

                double total = UserCartManager.UserCartTotalValue();

                ViewBag.TotalCartPrice = total;

                return View(userCart);
            }
            else {
                GlobalVariables.attemptedAccessURL = "~/Account/ViewCart";
                return Redirect("LogIn");
            }
        }

        public ActionResult ViewOrders() {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                List<OrderSummaryModel> userOrders = getUserOrders();
                return View(userOrders);
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Account/ViewOrders";
                return Redirect("LogIn");
            }
        }

        public ActionResult ViewOrderDetails(int parent_order = -1) {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                List<OrderDetailItemModel> orderDetails = getOrderDetails(parent_order);
                return View(orderDetails);
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Account/ViewOrders";
                return Redirect("LogIn");
            }
        }

        public ActionResult DeleteCartItem(EditCartItemModel item) {

            UserCartManager.deleteCartItem(item.Id,item.product);
            return Redirect("~/Account/ViewCart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateCartItem(UserCartItemModel item) {
            UserCartManager.updateCartItem(item.user_Id, item.product_id, item.quantity);
            return Redirect("~/Account/ViewCart");
        }

        //**********************Account settings********************************
        public ActionResult Settings()
        {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                return View();
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Account/Settings";
                return Redirect("LogIn");
            }    
        }

        public ActionResult ChangePassword()
        {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                return View();
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Account/ChangePassword";
                return Redirect("LogIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ModifyUserModel changes)
        {
                UserAccountManager.UpdateUserPassword(changes.Password);
                @ViewBag.Message = "Account Change was a success.";
                return View("PostAccountChange");          
        }
        public ActionResult ChangeEmail()
        {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                return View();
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Account/ChangeEmail";
                return Redirect("LogIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeEmail(ModifyUserModel changes) {

            UserAccountManager.UpdateUserEmail(changes.Email);
            @ViewBag.Message = "Account Change was a success.";
            return View("PostAccountChange");
        }     
    }
}