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
    /// Interaction logic for dlgBill.xaml
    /// </summary>
    public partial class dlgBill : Window
    {
        private bool m_EndDeal;

        public bool EndDeal
        {
            get { return m_EndDeal; }
            set { m_EndDeal = value; }
        }

        public dlgBill()
        {
            InitializeComponent();

            m_EndDeal = false;
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            Order check = DataContext as Order;

            check.Available = true;

            OrderAccessManager.GetInstance().AddOrder(check);

            m_EndDeal = true;

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
