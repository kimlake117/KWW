using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;
using KimsWoodWorking.BusinessLogic;
using static KimsWoodWorking.BusinessLogic.UserAccount;
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
            if (GlobalVariables.logInOut == "Log Out") {

                //reset global variables
                GlobalVariables.currentUser.UserName = "Account";
                GlobalVariables.logInOut = "Log In";
                GlobalVariables.currentUser.isSignedIn = false;
                GlobalVariables.currentUser.user_id = -1;
                GlobalVariables.currentUser.Email = "";


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
                if (UserAccount.pwMatch(user)) {

                    GlobalVariables.logInOut = "Log Out";
                    GlobalVariables.currentUser.isSignedIn = true;
                    GlobalVariables.currentUser.Email = user.Email;
                    GlobalVariables.currentUser.UserName = user.UserName;
                    GlobalVariables.currentUser.user_id = UserAccount.getUserId(user);

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

                List<UserCartItemModel> userCart = UserCart.getUserCart();

                double total = UserCart.UserCartTotalValue();

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

            UserCart.deleteCartItem(item.Id,item.product);
            return Redirect("~/Account/ViewCart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateCartItem(UserCartItemModel item) {
            UserCart.updateCartItem(item.user_Id, item.product_id, item.quantity);
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
                UserAccount.UpdateUserPassword(changes.Password);
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

            UserAccount.UpdateUserEmail(changes.Email);
            @ViewBag.Message = "Account Change was a success.";
            return View("PostAccountChange");
        }
        public ActionResult DeleteAccount()
        {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                return View();
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Account/DeleteAccount";
                return Redirect("LogIn");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(int user_id) {
            @ViewBag.Message = "Account Change was a success.";
            return View("PostAccountChange");
        }
        
    }
}