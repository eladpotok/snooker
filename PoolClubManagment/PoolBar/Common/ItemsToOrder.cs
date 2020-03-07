using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ItemsToOrder
    {
        public ItemsToOrder()
        {
        }

        public ItemsToOrder(int nAmount, int nItemID)
        {
            Amount = nAmount;
            ItemId = nItemID;
        }

        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public Item Item { get; set; }
    }
}
