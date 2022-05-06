using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class OrderDetailItemModel
    {
        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Display(Name = "Product Price")]
        public double product_price { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }
}