using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Common;
using DAL;

namespace BL
{
    public class ItemsCollectionProvider
    {
        #region SingleTone

        private static ItemsCollectionProvider m_instance;

        public static ItemsCollectionProvider GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ItemsCollectionProvider();
            }

            return m_instance;
        }

        #endregion

        #region Data Members

        private ObservableCollection<Item> m_ocItems;

        #endregion

        #region Properties

        public ObservableCollection<Item> Items
        {
            get { return m_ocItems; }
            set { m_ocItems = value; }
        }

        #endregion

        #region Public Methods

        public void UpdateItemsList(List<Item> lstNewItems)
        {
            foreach (Item item in lstNewItems)
            {
                Item itmOldItem = Items.FirstOrDefault(t =>
                    t.ItemId == item.ItemId);

                if (itmOldItem != null)
                {
                    MergeItems(item);
                }
                else
                {
                    //Items.Add(new Item(item.Name, item.Price, 0));
                    ItemAccessManager.GetInstance().AddItem(item);
                }
            }

            ObservableCollection<Item> ocTempItems =
                new ObservableCollection<Item>(Items);

            foreach (Item item in ocTempItems)
            {
                Item updateItem = lstNewItems.FirstOrDefault(t =>
                    t.ItemId == item.ItemId);

                // Check if the item has been deleted
                if (updateItem == null)
                {
                    //Items.Remove(item);
                    ItemAccessManager.GetInstance().DeleteItem(item);
                }
            }

            LoadItemsFromDB();
        }

        public void LoadItemsFromDB()
        {
            Items = new ObservableCollection<Item>(
                ItemAccessManager.GetInstance().GetAllItems());
        }

        #endregion

        #region Private Methods

        private void MergeItems(Item updateItem)
        {
            //oldItem.Name = updateItem.Name;
            //oldItem.Price = updateItem.Price;

            ItemAccessManager.GetInstance().UpdateItem(updateItem);
        }

        #endregion
    }
}
