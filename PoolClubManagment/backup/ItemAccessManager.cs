using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DAL
{
    public class ItemAccessManager
    {
        #region SingleTone

        private static ItemAccessManager m_instance;

        public static ItemAccessManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ItemAccessManager();
            }

            return m_instance;
        }

        #endregion

        public List<Item> GetAllItems()
        {
            List<Item> list = new List<Item>();

            using (PoolContext db = new PoolContext())
            {
                var query = from item in db.Items
                            select item;

                list = query.ToList<Item>();
            }

            return list;
        }

        public void AddItem(Item itToAdd)
        {
            using (PoolContext db = new PoolContext())
            {
                db.Items.AddObject(itToAdd);
                db.SaveChanges();
            }
        }

        public void DeleteItem(Item itToDelete)
        {
            using (PoolContext db = new PoolContext())
            {
                // Getting the item with given id
                Item toDelete =
                    db.Items.First(item => item.ItemId == itToDelete.ItemId);

                db.Items.DeleteObject(toDelete);
                db.SaveChanges();
            }
        }

        public void UpdateItem(Item itUpdated)
        {
            using (PoolContext db = new PoolContext())
            {
                // Getting the item with given id
                Item toUdpate = 
                    db.Items.FirstOrDefault(item => item.ItemId == itUpdated.ItemId);

                if (toUdpate != null)
                {
                    // Update the detaild
                    toUdpate.Name = itUpdated.Name;
                    toUdpate.Price = itUpdated.Price;

                    // Save changes
                    db.SaveChanges();
                }
            }
        }

        public void AddItems(List<Item> m_lstTempItems)
        {
            using (PoolContext db = new PoolContext())
            {
                foreach (Item item in m_lstTempItems)
                {
                    db.Items.AddObject(item);
                    db.SaveChanges();
                }
            }
        }
    }
}
