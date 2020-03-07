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
using Common;
using DAL;

namespace GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for dlgOrders.xaml
    /// </summary>
    public partial class dlgOrders : Window
    {
     

        public dlgOrders()
        {
            InitializeComponent();

            dtStart.SelectedDate = DateTime.Now;
            dtEnd.SelectedDate = DateTime.Now;

          
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((DateTime)dtEnd.SelectedDate < 
                    (DateTime)dtStart.SelectedDate)
                {
                    throw new Exception("תאריך ההתחלה גדול מתאיך הסיום");
                }

                List<Order> lstOrdersToShow = OrderAccessManager.GetInstance()
               .GetOrdersByDate((DateTime)dtStart.SelectedDate,
                               (DateTime)dtEnd.SelectedDate);

                

                //dgOrders.ItemsSource = lstOrdersToShow;
                lsbOrders.ItemsSource = lstOrdersToShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "אזהרה",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK,
                    MessageBoxOptions.RightAlign);
            }
           
        }
    }
}
