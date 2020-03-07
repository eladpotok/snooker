using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common
{
    public class Item : INotifyPropertyChanged
    {
        private bool m_bIsCostume;
        private decimal m_nPrice;

        public Item()
        {
        }

        public Item(string strName, decimal price)
        {
            Price = price;
            Name = strName;
        }

        public Item(string strName, decimal price, int nItemID, bool bIsCostume = false)
        {
            Name = strName;
            Price = price;
            ItemId = nItemID;
            m_bIsCostume = bIsCostume;
        }

        private bool m_bIsSelected;

      
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get { return m_nPrice; } set {
            m_nPrice = value;
            OnPropertyChanged("Price");
        
        } }
        public bool? IsCostume { get; set; }
        public bool IsSelected
        {
            get { return m_bIsSelected; }
            set { m_bIsSelected = value; OnPropertyChanged("IsSelected"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string strProeprtyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strProeprtyName));
            }
        }
    }
}
