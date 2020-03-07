using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DAL
{
    /// <summary>
    /// All items db access methods
    /// </summary>
    public class ItemAccessManager
    {
        #region SingleTone

        private static ItemAccessManager m_instance;

        /// <summary>
        /// Get the single instance of the class
        /// </summary>
        /// <returns></returns>
        public static ItemAccessManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ItemAccessManager();
            }

            return m_instance;
        }

        #endregion

        /// <summary>
        /// Get all the items from db
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Add item to db
        /// </summary>
        /// <param name="itToAdd"></param>
        public void AddItem(Item itToAdd)
        {
            // Checking that item with same name doesn't exist
            if (this.DoesItemNameAlreadyExists(itToAdd.Name))
            {
                throw new Exception("מוצר עם השם " + itToAdd.Name + " כבר קיים במערכת");
            }

            using (PoolContext db = new PoolContext())
            {
                db.Items.AddObject(itToAdd);

                this.SafeSave(db);
            }
        }


        /// <summary>
        /// Delete item from db
        /// </summary>
        /// <param name="itToDelete"></param>
        public void DeleteItem(Item itToDelete)
        {
            using (PoolContext db = new PoolContext())
            {
                // Getting the item with given id
                Item toDelete =
                    db.Items.FirstOrDefault(item => item.ItemId == itToDelete.ItemId);

                if (toDelete != null)
                {
                    db.Items.DeleteObject(toDelete);
                    this.SafeSave(db);
                }
            }
        }


        /// <summary>
        /// Update item in db
        /// </summary>
        /// <param name="itUpdated"></param>
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

                    // Checking if the name have to be updated
                    if (toUdpate.Name != itUpdated.Name)
                    {
                        // Checking that the new name doesn't already exists
                        if (this.DoesItemNameAlreadyExists(itUpdated.Name))
                        {
                            throw new Exception("מוצר עם השם " + itUpdated.Name + " כבר קיים במערכת");
                        }
                    }

                    toUdpate.Name = itUpdated.Name;
                    toUdpate.Price = itUpdated.Price;

                    // Save changes
                    this.SafeSave(db);
                }
            }
        }


        /// <summary>
        /// Add range of items to db
        /// </summary>
        /// <param name="m_lstTempItems"></param>
        public void AddItems(List<Item> m_lstTempItems)
        {
            using (PoolContext db = new PoolContext())
            {
                foreach (Item item in m_lstTempItems)
                {
                    db.Items.AddObject(item);
                }

                db.SaveChanges();
            }
        }


        /// <summary>
        /// Checking item with the given name already exists in db
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        private bool DoesItemNameAlreadyExists(string ItemName)
        {
            bool bDoesExists = false;

            using (PoolContext db = new PoolContext())
            {
                var query = from item in db.Items
                            where item.Name == ItemName
                            select item;

                if (query.Count() > 0)
                {
                    bDoesExists = true;
                }
            }

            return bDoesExists;
        }

        private void SafeSave(PoolContext db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: Write to log
                throw new Exception("אירעה שגיאה בשמירת הנתונים", ex);
            }
        }
    }
}
