using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KimsWoodWorking.Models;
using KimsWoodWorking.BusinessLogic;
using static KimsWoodWorking.BusinessLogic.UserCartManager;
using static KimsWoodWorking.BusinessLogic.OrderManager;


namespace KimsWoodWorking.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {
            List<UserCartItemModel> userCart = getUserCart();

            double total = UserCartTotalValue();

            ViewBag.TotalCartPrice = total;

            return View(userCart);
        }

        public ActionResult shipping_and_billing() {
            CheckOutViewModel model = new CheckOutViewModel();
            return View(model);
        }

        //this takes all the user info for an order and creates the order.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult shipping_and_billing(CheckOutViewModel com) {

            if (ModelState.IsValid) {
                createOrder(com);
                return View("OrderComplete");
            }
            return View("ErrorWithOrder");
        }
    }
}