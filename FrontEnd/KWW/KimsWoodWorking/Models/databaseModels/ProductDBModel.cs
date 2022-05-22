using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models.databaseModels
{
    public class ProductDBModel
    {
        public int product_id { get; set; } = -1;
        public string product_name { get; set; } = string.Empty;
        public string photo { get; set; } = string.Empty;
        public double product_price { get; set; } = -1;
        public string description { get; set; } = string.Empty;
        public int active { get; set; } = -1;
    }
}