using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace KimsWoodWorking.Models
{
    public class ProductModel
    {
        public int product_id { get; set; }

        [Display(Name ="Product Name")]
        public string product_name { get; set; }

        //path to the photo
        [Display(Name = "Photo")]
        public string photo { get; set; } = "";

        [Display(Name = "Product Price")]
        public double product_price { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        public int active { get; set; }
    }
}