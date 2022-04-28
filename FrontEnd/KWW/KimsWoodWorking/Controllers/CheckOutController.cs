using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;

namespace KimsWoodWorking.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {
            List<UserCartItemModel> userCart = SqliteDataAccess.getUserCart();

            double total = SqliteDataAccess.UserCartTotalValue();

            ViewBag.TotalCartPrice = total;

            return View(userCart); 
        }

        public ActionResult a() { 
            return View();
        }
    }
}