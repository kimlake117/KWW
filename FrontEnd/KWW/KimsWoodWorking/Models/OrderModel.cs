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
        [Required(ErrorMessage ="This Field is required.")]
        public string customer_first_name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This Field is required.")]
        public string customer_last_name { get; set; }


        [Display(Name = "Shipping Street Address")]
        [Required(ErrorMessage = "This Field is required.")]
        public string shipping_street_address { get; set; }


        [Display(Name = "Shipping City")]
        [Required(ErrorMessage = "This Field is required.")]
        public string shipping_city { get; set; }

        [Display(Name = "Shipping State")]
        [Required(ErrorMessage = "This Field is required.")]
        public int shipping_state { get; set; }

        [Display(Name = "Shipping Postal Code")]
        [Required(ErrorMessage = "This Field is required.")]
        public int shipping_postal_code { get; set; }

        [Display(Name = "Billing First Name")]
        [Required(ErrorMessage = "This Field is required.")]
        public string billing_first_name { get; set; }

        [Display(Name = "Billing Last Name")]
        [Required(ErrorMessage = "This Field is required.")]
        public string billing_last_name { get; set; }

        [Display(Name = "Billing Street Address")]
        [Required(ErrorMessage = "This Field is required.")]
        public string billing_street_address { get; set; }

        [Display(Name = "Billing City")]
        [Required(ErrorMessage = "This Field is required.")]
        public string billing_city { get; set; }

        [Display(Name = "Billing State")]
        [Required(ErrorMessage = "This Field is required.")]
        public int billing_state { get; set; }

        [Display(Name = "Billing Postal Code")]
        [Required(ErrorMessage = "This Field is required.")]
        public int billing_postal_code { get; set; }

        [Display(Name = "Credit Card Number")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_number { get; set; }

        [Display(Name = "Experation Month")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_exp_month { get; set; }

        [Display(Name = "Experation Year")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_exp_year { get; set; }

        [Display(Name = "CCV")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_cvc { get; set; }

    }
}