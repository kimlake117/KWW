using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KimsWoodWorking.Models
{
    public class EditCartItemModel
    {
        public int Id { get; set; }
        public int product { get; set; }
        public int quantity { get; set; }
    }
}