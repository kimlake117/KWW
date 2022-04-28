using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace KimsWoodWorking.Models
{
    public class ProductModel
    {
        public int product_id { get; set; }

        public string product_name { get; set; }

        //path to the photo
        public string photo { get; set; }

        public double product_price { get; set; }

        public string description { get; set; }
    }
}