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
            List<ProductModel> testList = new List<ProductModel>();

            testList.Add(new ProductModel { Product_Id = 1, ProductName = "Picture Frame", ProductDescription = "for displaying pictures", ProductPrice = 350 });

            return View(testList);
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