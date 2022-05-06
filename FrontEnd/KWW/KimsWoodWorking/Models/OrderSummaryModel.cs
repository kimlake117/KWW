using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class OrderSummaryModel
    {
        [Display(Name = "Order Number")]
        public int parent_order_id { get; set; }

        [Display(Name = "Order Total")]
        public double total_order_cost { get; set; }

        [Display(Name = "Order Date")]
        public string order_date { get; set; }

        [Display(Name = "Order Status")]
        public string order_status_description { get; set; }

        [Display(Name = "Order Status Date")]
        public string status_date { get; set; }

        [Display(Name = "Shipping Name")]
        public string shipping_name { get; set; }

        [Display(Name = "Shipping Street Address")]
        public string shipping_street_address { get; set; }

        [Display(Name = "Shipping City")]
        public string shipping_city { get; set; }

        [Display(Name = "Shipping State")]
        public string shipping_state { get; set; }

        [Display(Name = "Shipping Postal Code")]
        public int shipping_postal_code { get; set; }
    }
}