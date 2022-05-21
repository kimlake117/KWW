using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace KimsWoodWorking.Models.ViewModels
{
    public class EditOrderViewModel
    {
        public OrderSummaryModel Order { get; set; }

        public List<OrderDetailItemModel> productsList { get; set; }
    }
}