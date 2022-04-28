using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models
{
    public class OrderModel
    {
        public string customer_first_name { get; set; }

        public string customer_last_name { get; set; }

        public string shipping_street_address { get; set; }

        public string shipping_city { get; set; }

        public int shipping_state { get; set; }

        public int shipping_postal_code { get; set; }

        public string billing_first_name { get; set; }

        public string billing_last_name { get; set; }

        public string billing_street_address { get; set; }

        public string billing_city { get; set; }

        public int billing_state { get; set; }

        public int billing_postal_code { get; set; }

        public int cc_number { get; set; }

        public int cc_exp_month { get; set; }

        public int cc_exp_year { get; set; }

        public int cc_cvc { get; set; }

    }
}