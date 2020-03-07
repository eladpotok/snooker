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
using System.Windows.Shapes;
using DAL;
using BL;
using Common;
using System.ComponentModel;

namespace GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for dlgOrderCheck.xaml
    /// </summary>
    public partial class dlgOrderCheck : Window 
    {
        public delegate void OnOrderMovedDelegate(Order order);
        public event OnOrderMovedDelegate OnOrderMoved;

        public dlgOrderCheck()
        {
            InitializeComponent();

            //DBManagment.GetInstance().Items.Add(new Item("אחר", true));
            //ItemAccessManager.GetInstance().AddItem(new Common.Item("אחר", true));

            //lsbProducts.ItemsSource = DBManagment.GetInstance().Items;
            lsbProducts.ItemsSource = 
                ItemsCollectionProvider.GetInstance().Items;
        }

        internal void btnChooseProducts_Click(object sender, RoutedEventArgs e)
        {
            Order order = DataContext as Common.Order;
            Button buttonSender = sender as Button;

            Item item = buttonSender.DataContext as Common.Item;

            // Set the amount of the item
            //TODO: Change the amount of the item
            //item.Amount = ucNumericText.Value;
             // TODO: Change amount
            int nAmount = ucNumericText.Value;
            // Check if the item is costume or existing item
            if (item.IsCostume != null &&
                (bool)item.IsCostume)
            {
                dlgInputPrice dlgInputPrice = new dlgInputPrice();
                dlgInputPrice.ShowDialog();

                // Check if the price accepted
                if (dlgInputPrice.IsAccepted)
                {

                    ItemsToOrder itemToOrder = new ItemsToOrder(nAmount,
                        new Item(item.Name, dlgInputPrice.Price,item.ItemId, true));
                    //order.ItemsToOrder.Add(itemToOrder);
                    order.AddItemToOrder(itemToOrder);
                }
            }
            else
            {
                // Get the item in case that it exists in the order
                ItemsToOrder itoItem = order.ItemsToOrder.FirstOrDefault(t =>
                    t.ItemId == item.ItemId);

                // Check if the item exists in the order
                if (itoItem != null)
                {
                    //itoItem = new ItemsToOrder(nAmount + itoItem.Amount,
                    //    new Item(item.Name, item.Price, item.ItemId));
                    order.ChangeAmount(itoItem, nAmount);
                }
                else
                {
                    ItemsToOrder itemToOrder = new ItemsToOrder(nAmount,
                        new Item(item.Name, item.Price, item.ItemId));
                    order.AddItemToOrder(itemToOrder);
                }
            }

            
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            Order order = DataContext as Order;

            order.Available = false;

            // Check if the place is pool table
            if (order.IsTableOrder)
            {
                // Start pool game
                order.StartPoolTable();
            }
            
            Close();
        }

        private void btnCloseBill_Click(object sender, RoutedEventArgs e)
        {
            Order check = DataContext as Order;

            dlgBill bill = new dlgBill();
            bill.DataContext = check;

            // Stop or start the pool game timer
            check.StopOrStartTime();

            bill.ShowDialog();

            if (bill.EndDeal)
            {
                Close();
            }
            else
            {
                // Start the game time again
                check.StopOrStartTime();
            }


            //check.Available = true;
          
            //OrderAccessManager.GetInstance().AddOrder(check);

            //Close();
        }

        private void btnPrintBill_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveProducts_Click(object sender, RoutedEventArgs e)
        {
            // Create temporary items list to the removed items
            List<ItemsToOrder> items = new List<ItemsToOrder>();

            Order check = DataContext as Order;

            // Iterate over the items
            foreach (ItemsToOrder item in check.ItemsToOrder)
            {
                if (item.Item.IsSelected)
                {
                    items.Add(item);
                }
            }

            foreach (ItemsToOrder item in items)
            {
                check.RemoveItemToOrder(item);
            }
        }

        private void chbSelectOrder_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            ItemsToOrder item = checkBox.DataContext as ItemsToOrder;

            item.Item.IsSelected = true;
        }

        private void chbSelectOrder_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            ItemsToOrder item = checkBox.DataContext as ItemsToOrder;

            item.Item.IsSelected = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //DBManagment.GetInstance().Items.RemoveAt(DBManagment.GetInstance().Items.Count - 1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Order order = this.DataContext as Order;

            if (order.Available)
            {
                dlgInputUser dlgInputUser = new dlgInputUser();
                dlgInputUser.ShowDialog();

                // Set the name of the order
                order.Name = dlgInputUser.NameValue;
            }
        }

        private void btnMergeOrders_Click(object sender, RoutedEventArgs e)
        {
            //dlgPlacesList dlgMergeList = new dlgPlacesList();

            //dlgMergeList.ShowDialog();

            //if (dlgMergeList.IsAccpeted)
            //{ 
                
            //}

            this.Opacity = 0.2;
        }

        private void btnMoveOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = this.DataContext as Order;

            if (OnOrderMoved != null)
            {
                OnOrderMoved(order);
            }

            Close();
        }

        private void btnStopTime_Click(object sender, RoutedEventArgs e)
        {
            Order check = DataContext as Order;

            check.StopOrStartTime();
        }
    }
}
