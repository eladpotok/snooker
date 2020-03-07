using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using BL;
using System.ComponentModel;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for ucPlace.xaml
    /// </summary>
    public partial class ucPlace : UserControl, INotifyPropertyChanged
    {
        public static int m_nIndexTable = 1;

        private bool m_bIsPoolTable;
        private PresentationObject m_ppPlaceType;
        private bool m_bIsSelected;
        private string m_strPlaceName;
        private bool m_bIsSittingPlace;

        public bool IsSittingPlace
        {
            get { return m_bIsSittingPlace; }
            set { m_bIsSittingPlace = value; }
        }

        public string PlaceName
        {
            get { return m_strPlaceName; }
            set { m_strPlaceName = value; OnPropertyChanged("PlaceName"); }
        }

        public bool IsSelected
        {
            get { return m_bIsSelected; }
            set { m_bIsSelected = value; OnPropertyChanged("IsSelected"); }
        }

        public PresentationObject PlaceType
        {
            get { return m_ppPlaceType; }
            set { m_ppPlaceType = value; }
        }

        public bool IsAvailable
        {
            get
            {
                Order check = this.DataContext as Order;
            return check.Available;
            }
        }

        public ucPlace(ImageSource strImageSource, bool bIsPoolTable, PresentationObject ppType,
             bool bIsSittingPlace = true)
        {
            InitializeComponent();

            // Set the image source of the user control
            SetImage(strImageSource);

            if (ppType == PresentationObject.BAR_CHAIR)
            {
                PlaceName = "כיסא ";
            }
            if (ppType == PresentationObject.POOL_TABLE)
            {
                PlaceName = "שולחן ";
            }

            PlaceName += m_nIndexTable++;

            // Set local members
            this.m_bIsPoolTable = bIsPoolTable;
            this.m_ppPlaceType = ppType;
            this.m_bIsSittingPlace = bIsSittingPlace;

            // Open the check
            OpenCheck();

            // Check if it's sitting place
            if (!bIsSittingPlace)
            {
                this.imgBusyStatus.Visibility = System.Windows.Visibility.Collapsed;
                this.txbName.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Set the image source of the user control
        /// </summary>
        /// <param name="strImageSource">The image source</param>
        private void SetImage(ImageSource strImageSource)
        {
          //  Uri uri = new Uri(strImageSource);

            //BitmapImage bitmap = new BitmapImage(uri);

            this.imgView.Source = strImageSource;
        }

        /// <summary>
        /// Open the check for this place
        /// </summary>
        internal void OpenCheck()
        {
            //this.DataContext = new Check(m_bIsPoolTable);
            this.DataContext = new Order(m_bIsPoolTable);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string strProeprtyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strProeprtyName));
            }
        }

        private void uc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void chbSelected_Checked(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }

        private void chbSelected_Unchecked(object sender, RoutedEventArgs e)
        {
            IsSelected = false;
        }

        internal void MoveDataContext(Order order)
        {
            Order currOrder = DataContext as Order;

            if (currOrder.IsTableOrder &&
                !order.IsTableOrder)
            {
                this.DataContext = order;
                order.IsTableOrder = currOrder.IsTableOrder;
                order.StopOrStartTime();
            }

            if (!currOrder.IsTableOrder &&
                order.IsTableOrder)
            {
                this.DataContext = order;
                order.IsTableOrder = currOrder.IsTableOrder;
                order.StopOrStartTime();
            }

            if ((currOrder.IsTableOrder && order.IsTableOrder) ||
                !currOrder.IsTableOrder && !order.IsTableOrder)
            {
                this.DataContext = order;
            }
        }

        internal void MergeDataContext(Order newOrder)
        {
            Order oldOrder = DataContext as Order;

            foreach (ItemsToOrder item in newOrder.ItemsToOrder)
            {
                // Get the item in case that it exists in the order
                ItemsToOrder itoItem = oldOrder.ItemsToOrder.FirstOrDefault(t =>
                    t.ItemId == item.ItemId);

                // Check if the item exists in the order
                if (itoItem != null)
                {
                    //itoItem = new ItemsToOrder(nAmount + itoItem.Amount,
                    //    new Item(item.Name, item.Price, item.ItemId));
                    oldOrder.ChangeAmount(itoItem, item.Amount);
                }
                else
                {
                    oldOrder.AddItemToOrder(item);
                }
            }

            if (oldOrder.IsTableOrder &&
                !newOrder.IsTableOrder)
            {
                oldOrder.IsTableOrder = newOrder.IsTableOrder;
                newOrder.StopOrStartTime();
            }

            if (!oldOrder.IsTableOrder &&
                newOrder.IsTableOrder)
            {
                oldOrder.IsTableOrder = newOrder.IsTableOrder;
                newOrder.StopOrStartTime();
            }
        }
    }
}
