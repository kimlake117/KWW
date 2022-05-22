using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using KimsWoodWorking.Models.databaseModels;
using static KimsWoodWorking.BusinessLogic.StateManager;
using static KimsWoodWorking.BusinessLogic.MonthYearManager;

namespace KimsWoodWorking.Models
{
    public class CheckOutViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This Field is required.")]
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

        //the state the user selects for shipping
        [Display(Name = "Shipping State")]
        [Required(ErrorMessage = "This Field is required.")]
        public int shipping_state { get; set; }

        //the list of states
        [Display(Name = "Shipping State")]
        [Required(ErrorMessage = "This Field is required.")]
        public IEnumerable<StateModelDB> StatesList { get; set; } = getStates();

        [Display(Name = "Shipping Postal Code")]
        [DataType(DataType.PostalCode)]
        [Range(10000,99999,ErrorMessage ="Please enter a valid postal code.")]
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
        [DataType(DataType.PostalCode)]
        [Range(10000, 99999, ErrorMessage = "Please enter a valid postal code.")]
        [Required(ErrorMessage = "This Field is required.")]
        public int billing_postal_code { get; set; }

        [Display(Name = "Credit Card Number")]
        [Range(1000000000000000,9999999999999999,ErrorMessage ="Please enter a vaild card number.")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_number { get; set; }

        [Display(Name = "Experation Month")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_exp_month { get; set; }

        [Display(Name = "Experation Year")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_exp_year { get; set; }

        [Display(Name = "CCV")]
        [Range(100,999,ErrorMessage = "Please enter a valid CVC number.")]
        [Required(ErrorMessage = "This Field is required.")]
        public int cc_cvc { get; set; }

        public List<MonthModel> months_list { get; set; } = fillMonthList();

        public List<int> year_list { get; set; } = getYearList();

    }
}