using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using static KimsWoodWorking.BusinessLogic.StateManager;
using static KimsWoodWorking.BusinessLogic.OrderManager;

namespace KimsWoodWorking.Models.ViewModels
{
    public class SearchOrderViewModel
    {
        public List<OrderSummaryModel> OrderSummaries { get; set; } = new List<OrderSummaryModel>();
        public OrderSummaryModel Order { get; set; } = setUpOrder();
        public List<StateModelDB> StatesList { get; set; } = getStates();
        public List<OrderStatusDBModel> OrderStatusList { get; set; } = getOrderStatusOptions();

        private static OrderSummaryModel setUpOrder() {
            var newOrder= new OrderSummaryModel();
            newOrder.order_status_description = "Select Status";

            return newOrder;
        }
    }
}