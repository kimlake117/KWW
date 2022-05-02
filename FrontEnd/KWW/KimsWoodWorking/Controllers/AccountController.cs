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
        public ActionResult Index()
        {
            return View("LogIn");
        }

        public ActionResult LogIn()
        {
            if (GlobalVariables.logInOut == "Log Out") {


                GlobalVariables.CurrentUser_Name = "Account";
                GlobalVariables.logInOut = "Log In";
                GlobalVariables.isSignedIn = false;
                GlobalVariables.CurrentUser_id = -1;
                GlobalVariables.CurrentUser_Email = "";


                return View("~/Views/Home/Index.cshtml");
            }
            else {
                return View();
            }
        }

        [HttpPost]
        public ActionResult LogIn(UserModel user)
        {
            if (ModelState.IsValid) {
                if (UserAccount.pwMatch(user)) {

                    GlobalVariables.logInOut = "Log Out";
                    GlobalVariables.isSignedIn = true;
                    GlobalVariables.CurrentUser_Email=user.Email;
                    GlobalVariables.CurrentUser_Name = user.UserName;
                    GlobalVariables.CurrentUser_id = UserAccount.getUserId(user);

                    return View("~/Views/Home/Index.cshtml");
                }
            }
            return View("invalidLogIn");
        }

        public ActionResult ViewCart()
        {
            if (GlobalVariables.isSignedIn) {

                List<UserCartItemModel> userCart = UserCart.getUserCart();

                double total = UserCart.UserCartTotalValue();

                ViewBag.TotalCartPrice = total;

                return View(userCart);
            }
            else {
                return Redirect("LogIn");
            }
        }

        public ActionResult ViewOrders() { 
        
            return View();
        }

        public ActionResult Settings()
        {

            return View();
        }

        public ActionResult SignUp() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(NewUserModel newUser)
        {
            if (ModelState.IsValid) {
                int recordsCreated = CreateNewUser(newUser.UserName,newUser.Email,newUser.Password);
            }
            UserModel user = new UserModel();

            user.UserName = newUser.UserName;
            user.Email = newUser.Email;
            user.Password = newUser.Password;

            LogIn(user);

            return View("~/Views/Home/Index.cshtml");
        }
    }
}