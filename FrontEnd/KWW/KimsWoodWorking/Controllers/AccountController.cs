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
        // GET: Account
        public ActionResult Index()
        {
            return View("LogIn");
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult ViewCart()
        {
            List<UserCartItemModel> userCart = UserCart.getUserCart();

            double total = UserCart.UserCartTotalValue();

            ViewBag.TotalCartPrice = total;

            return View(userCart);
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

            return View();
        }
    }
}