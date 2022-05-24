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
        [Range(0,int.MaxValue,ErrorMessage ="Please enter a positive number.")]
        public int quantity { get; set; }

        public int product_id { get; set; }

        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Display(Name = "Price")]
        public double product_price { get; set; }
        

        
    }
}