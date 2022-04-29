using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models
{
    public class OrderModel
    {
        [Display(Name ="First Name")]
        public string customer_first_name { get; set; }

        [Display(Name = "Last Name")]
        public string customer_last_name { get; set; }


        [Display(Name = "Shipping Street Address")]
        public string shipping_street_address { get; set; }


        [Display(Name = "Shipping City")]
        public string shipping_city { get; set; }

        [Display(Name = "Shipping State")]
        public int shipping_state { get; set; }

        [Display(Name = "Shipping Postal Code")]
        public int shipping_postal_code { get; set; }

        [Display(Name = "Billing First Name")]
        public string billing_first_name { get; set; }

        [Display(Name = "Billing Last Name")]
        public string billing_last_name { get; set; }

        [Display(Name = "Billing Street Address")]
        public string billing_street_address { get; set; }

        [Display(Name = "Billing City")]
        public string billing_city { get; set; }

        [Display(Name = "Billing State")]
        public int billing_state { get; set; }

        [Display(Name = "Billing Postal Code")]
        public int billing_postal_code { get; set; }

        [Display(Name = "Credit Card Number")]
        public int cc_number { get; set; }

        [Display(Name = "Experation Month")]
        public int cc_exp_month { get; set; }

        [Display(Name = "Experation Year")]
        public int cc_exp_year { get; set; }

        [Display(Name = "CCV")]
        public int cc_cvc { get; set; }

    }
}