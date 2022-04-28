using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;

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
            List<UserCartItemModel> userCart = SqliteDataAccess.getUserCart();

            return View(userCart);
        }

        public ActionResult ViewOrders() { 
        
            return View();
        }

        public ActionResult Settings()
        {

            return View();
        }
    }
}