﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DAL
{
    public class OrderAccessManager
    {
        public List<Order> GetAllOrders()
        {
            List<Order> list = new List<Order>();

            using (PoolContext db = new PoolContext())
            {
                var query = from order in db.Orders.Include("ItemsToOrder.Item")
                            select order;

                list = query.ToList<Order>();
            }

            return list;
        }

        public void AddOrder(Order orderToAdd)
        {
            using (PoolContext db = new PoolContext())
            {
                db.Orders.AddObject(orderToAdd);
                db.SaveChanges();
            }
        }

        public void DeleteOrder(Order orderToDelete)
        {
            using (PoolContext db = new PoolContext())
            {
                // Getting the item with given id
                Order toDelete =
                    db.Orders.First(order => order.OrderId == orderToDelete.OrderId);

                db.Orders.DeleteObject(toDelete);
                db.SaveChanges();
            }
        }

        public void UpdateOrder(Order orderToUpdate)
        {
            using (PoolContext db = new PoolContext())
            {
                // Getting the item with given id
                Order toUpdate =
                    db.Orders.Include("ItemsToOrder.Item").First(order => order.OrderId == orderToUpdate.OrderId);

                toUpdate.Date = orderToUpdate.Date;
                toUpdate.GameHoursTime = orderToUpdate.GameHoursTime;
                
                // Compare the list of items to order
                int nIndex = 0;
                List<int> toDeleteIndexes = new List<int>();

                foreach (ItemsToOrder current1 in toUpdate.ItemsToOrder)
                {
                    bool bIsStillExists = false;
                    

                    // Checking it exists in the update list
                    foreach (ItemsToOrder current2 in orderToUpdate.ItemsToOrder)
                    {
                        if ((current1.ItemId == current2.ItemId) && 
                            (current1.OrderId == current2.OrderId))
                        {
                            current1.Amount = current2.Amount;
                            bIsStillExists = true;

                            break;
                        }
                    }


                    if (!bIsStillExists)
                    {
                        toDeleteIndexes.Add(nIndex);
                    }

                    ++nIndex;
                }

                // Delete all the not existsing elements
                foreach(int nCurrentIndex in toDeleteIndexes)
                {
                    toUpdate.ItemsToOrder.RemoveAt(nCurrentIndex);
                }


                List<ItemsToOrder> lstToAdd = new List<ItemsToOrder>();

                foreach (ItemsToOrder current1 in orderToUpdate.ItemsToOrder)
                {
                    bool bIsNewElement = true;

                    // Checking it exists in the update list
                    foreach (ItemsToOrder current2 in toUpdate.ItemsToOrder)
                    {
                        if ((current1.ItemId == current2.ItemId) &&
                            (current1.OrderId == current2.OrderId))
                        {
                            current1.Amount = current2.Amount;
                            bIsNewElement = false;
                            break;
                        }
                    }


                    if (bIsNewElement)
                    {
                        lstToAdd.Add(current1);
                    }              
                }

                // Adding all the new elements
                foreach (ItemsToOrder currentToAdd in lstToAdd)
                {
                    toUpdate.ItemsToOrder.Add(currentToAdd);
                }

                db.SaveChanges();
            }
        }
    }
}
