using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class UserCartItemModel
    {
        public int user_Id { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        public int product_id { get; set; }
        public string product_name { get; set; }
        public double product_price { get; set; }
        

        
    }
}