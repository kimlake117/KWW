using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using static KimsWoodWorking.BusinessLogic.ProductManager;
using static KimsWoodWorking.BusinessLogic.UserCart;

namespace KimsWoodWorking.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            //this makes the sign in function better
            if (GlobalVariables.currentUser.isSignedIn)
            {
                GlobalVariables.attemptedAccessURL = "~/Home/Index";
            }
            return View(GetProductList());
        }

        public ActionResult addToCart(int productID) {
            if (GlobalVariables.currentUser.isSignedIn)
            {
                addProductToCart(productID);
                return Redirect("~/Account/ViewCart");
            }
            else
            {
                GlobalVariables.attemptedAccessURL = "~/Products/";
                return Redirect("~/Account/LogIn");
            }
        }
    }
}