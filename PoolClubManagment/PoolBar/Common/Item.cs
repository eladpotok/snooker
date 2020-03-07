using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Item
    {
        public Item()
        {
        }

        public Item(string strName, double price)
        {
            Price = price;
            Name = strName;
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
