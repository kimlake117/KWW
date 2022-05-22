using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace KimsWoodWorking.Models
{
    public class ProductModel
    {
        public int product_id { get; set; } = -1;

        [Display(Name ="Product Name")]
        public string product_name { get; set; } = String.Empty;

        //path to the photo
        [Display(Name = "Photo")]
        public string photo { get; set; } = String.Empty;

        [Display(Name = "Product Price")]
        public double product_price { get; set; } = -1;

        [Display(Name = "Description")]
        public string description { get; set; } = string.Empty;

        [Range (-1,1)]
        public int active { get; set; } = -1;
    }
}