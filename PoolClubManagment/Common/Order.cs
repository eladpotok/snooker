using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common
{
    public class Order : INotifyPropertyChanged
    {
        #region Data Members

        private float m_fTotalPrice;
        private float m_fCostForGameHour;
        private bool m_bAvailable;
        private bool m_bIsPoolTable;
        private bool m_bIsTimerOn;
        private DateTime m_dtGameTime;
        private Timer m_tmGameTimer;
        private string m_strOrderName;

        #endregion

        #region Properties

        public DateTime GameTime
        {
            get { return m_dtGameTime; }
            set { m_dtGameTime = value; OnPropertyChanged("GameTime"); }
        }

        public float CostForGameHour
        {
            get { return m_fCostForGameHour; }
            set { m_fCostForGameHour = value; }
        }   

        public bool IsTableOrder
        {
            get { return m_bIsPoolTable; }
            set { m_bIsPoolTable = value; OnPropertyChanged("IsPoolTable"); }
        }

        public bool Available
        {
            get { return m_bAvailable; }
            set { m_bAvailable = value; OnPropertyChanged("Available"); }
        }

        public bool IsTimerOn
        {
            get { return m_bIsTimerOn; }
            set { m_bIsTimerOn = value; OnPropertyChanged("IsTimerOn"); }
        }

        public float TotalPrice
        {
            get { return m_fTotalPrice; }
            set { m_fTotalPrice = value; OnPropertyChanged("TotalPrice"); }
        }

        #endregion

        public Order(bool bIsPoolTable)
        {
            ItemsToOrder = new ObservableCollection<ItemsToOrder>();
            Date = DateTime.Now;

            TotalPrice = 0;
            Available = true;
            IsTableOrder = bIsPoolTable;
            CostForGameHour = 0;
            GameHoursTime = "0";
        }

        public Order()
        {

        }

        public int OrderId { get ;  set; }
        public DateTime Date { get; set; }
        public ObservableCollection<ItemsToOrder> ItemsToOrder { get; set; }
        public string GameHoursTime { get; set; }
        public string Name 
        { 
            get { return this.m_strOrderName; } 
            set {
                this.m_strOrderName = value;
                OnPropertyChanged("Name"); 
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string strProeprtyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strProeprtyName));
            }
        }

        public void StartPoolTable()
        {
            if (m_tmGameTimer == null)
            {
                m_tmGameTimer = new Timer();
                m_tmGameTimer.Interval = 1000;
                m_tmGameTimer.Elapsed += m_tmGameTimer_Elapsed;
                m_tmGameTimer.Start();

                IsTimerOn = true;

                //TODO: Turn on the light
            }
        }

        void m_tmGameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GameTime = GameTime.AddSeconds(1);

            GameHoursTime = GameTime.TimeOfDay.ToString();
        }

        public void AddItemToOrder(ItemsToOrder itemToOrder)
        {
            ItemsToOrder.Add(itemToOrder);
            OnPropertyChanged("ItemsToOrder");
        }

        public void RemoveItemToOrder(ItemsToOrder item)
        {
            ItemsToOrder.Remove(item);
            OnPropertyChanged("ItemsToOrder");
        }

        public void UpateItemToOrder()
        {
            OnPropertyChanged("ItemsToOrder");
        }

        public void ChangeAmount(ItemsToOrder item, int nAmount)
        {
            item.Amount += nAmount;

            //item = new ItemsToOrder(nAmount + item.Amount, item.Item);
            OnPropertyChanged("ItemsToOrder");
        }

        public void StopOrStartTime()
        {
            if (m_tmGameTimer != null)
            {
                if (IsTimerOn)
                {
                    m_tmGameTimer.Stop();
                }
                else
                {
                    m_tmGameTimer.Start();
                }

                IsTimerOn = !IsTimerOn;
            }
            else
            {
                StartPoolTable();
            }
        }
    }
}
