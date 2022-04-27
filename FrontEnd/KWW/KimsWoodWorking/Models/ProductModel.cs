using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models
{
    public class ProductModel
    {
        public int Product_Id { get; set; }

        public string ProductName { get; set; }

        public string PhotoPath { get; set; }

        public double ProductPrice { get; set; }

        public string ProductDescription { get; set; }
    }
}