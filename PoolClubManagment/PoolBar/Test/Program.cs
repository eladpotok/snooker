using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ItemAccessManager itemsManager = new ItemAccessManager();
            OrderAccessManager orderManager = new OrderAccessManager();

            
            List<Item> items = itemsManager.GetAllItems();
            Order order1 = new Order();
            //ItemsToOrder itemToOrder = new ItemsToOrder(2, items[0].ItemId);
            //ItemsToOrder itemToOrder2 = new ItemsToOrder(4, items[1].ItemId);
            ItemsToOrder itemToOrder = new ItemsToOrder(5, items[2].ItemId);

            List<Order> orders = orderManager.GetAllOrders();
            orders[0].ItemsToOrder.RemoveAt(0);
            orders[0].ItemsToOrder.Add(itemToOrder);
            orderManager.UpdateOrder(orders[0]);
            //order1.ItemsToOrder.Add(itemToOrder);
            //order1.ItemsToOrder.Add(itemToOrder2);
            //orderManager.AddOrder(order1);

        }
    }
}
