using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common
{
    public class ItemsToOrder : INotifyPropertyChanged
    {
        private int m_nAmount;
        private Item m_itmItem;

        public ItemsToOrder()
        {
        }

        public ItemsToOrder(int nAmount, Item item)
        {
            Amount = nAmount;
            Item = item;
            ItemId = item.ItemId;
        }

        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get { return m_nAmount; } set {
            m_nAmount = value;
            OnPropertyChanged("Amount"); 
        } }
        public Item Item { get { return m_itmItem; } set {
            m_itmItem = value;
                //OnPropertyChanged("Item");
        
        } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string strProeprtyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strProeprtyName));
            }
        }
    }
}
