using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;
using KimsWoodWorking.BusinessLogic;
using static KimsWoodWorking.BusinessLogic.UserAccount;



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
            //if the user was already logged in, then log them out
            if (GlobalVariables.logInOut == "Log Out") {

                //reset global variables
                GlobalVariables.CurrentUser_Name = "Account";
                GlobalVariables.logInOut = "Log In";
                GlobalVariables.isSignedIn = false;
                GlobalVariables.CurrentUser_id = -1;
                GlobalVariables.CurrentUser_Email = "";


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
                    GlobalVariables.isSignedIn = true;
                    GlobalVariables.CurrentUser_Email=user.Email;
                    GlobalVariables.CurrentUser_Name = user.UserName;
                    GlobalVariables.CurrentUser_id = UserAccount.getUserId(user);

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
            if (GlobalVariables.isSignedIn) {

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
        
            return View();
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
            if (GlobalVariables.isSignedIn)
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
            if (GlobalVariables.isSignedIn)
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

            if (GlobalVariables.isSignedIn)
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
            if (GlobalVariables.isSignedIn)
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