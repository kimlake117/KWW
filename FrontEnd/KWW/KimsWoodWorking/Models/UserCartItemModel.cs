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
        public int Id { get; set; }
        public ProductModel Product { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }
}