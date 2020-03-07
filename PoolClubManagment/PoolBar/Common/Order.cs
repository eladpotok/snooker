using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Order
    {
        public Order()
        {
            ItemsToOrder = new List<ItemsToOrder>();
            Date = DateTime.Now;
        }

        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public List<ItemsToOrder> ItemsToOrder { get; set; }
        public double? GameHoursTime { get; set; }
    }
}
