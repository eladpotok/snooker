using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DAL
{
    public class ItemAccessManager
    {
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
                    db.Items.First(item => item.ItemId == itUpdated.ItemId);

                // Update the detaild
                toUdpate.Name = itUpdated.Name;
                toUdpate.Price = itUpdated.Price;

                // Save changes
                db.SaveChanges();
            }
        }
    }
}
