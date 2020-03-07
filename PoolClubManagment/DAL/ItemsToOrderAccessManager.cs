using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DAL
{
    public class ItemsToOrderAccessManager
    {
        #region SingleTone

        private static ItemsToOrderAccessManager m_instance;

        public static ItemsToOrderAccessManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ItemsToOrderAccessManager();
            }

            return m_instance;
        }

        #endregion

        public void AddItemsToOrder(ItemsToOrder toAdd)
        {
            using (PoolContext db = new PoolContext())
            {
                db.ItemsToOrders.AddObject(toAdd);
                db.SaveChanges();
            }
        }

        public void DeleteItemsToOrder(ItemsToOrder toDelete)
        {
        }

        public void UpdateItemsToOrder(ItemsToOrder toUpdate)
        {
        }
    }
}
