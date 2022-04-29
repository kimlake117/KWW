using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models.databaseModels
{
    public class UserCartDBModel
    {
        public int user_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}