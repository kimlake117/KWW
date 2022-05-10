using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models.databaseModels
{
    public class ProductDBModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string photo { get; set; }
        public double product_price { get; set; }
        public string description { get; set; }
        public int active { get; set; }
    }
}