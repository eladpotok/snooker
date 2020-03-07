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
using Common;
using System.Collections.ObjectModel;
using BL;
using GUI.UserControls;

namespace GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for dlgAddProducts.xaml
    /// </summary>
    public partial class dlgAddProducts : Window
    {
        private List<Common.Item> m_lstTempItems;

        public dlgAddProducts()
        {
            InitializeComponent();

            ObservableCollection<Common.Item> lstItems =
                ItemsCollectionProvider.GetInstance().Items;

            this.DataContext = lstItems;

            m_lstTempItems = new List<Common.Item>();

            foreach (Common.Item item in lstItems)
            {
                m_lstTempItems.Add(new Common.Item(
                    item.Name, item.Price, item.ItemId));
            }

            dgProducts.ItemsSource = m_lstTempItems;

            SetPricePerMin();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            // Update the run-time items list
            ItemsCollectionProvider.GetInstance().
                UpdateItemsList(m_lstTempItems);

            Dictionary<int, double> dicPricePerMin =
                new Dictionary<int, double>();

            foreach (ucPricePerMin item in spPrices.Children)
            {
                dicPricePerMin.Add(item.Min, item.Price);
            }

            SettingsXmlProvider.WritePriceGame(dicPricePerMin);

            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridColumn column = null;

            if (dgProducts.Columns.Count > 0)
            {
                column = dgProducts.Columns[0];
                column.Visibility = System.Windows.Visibility.Collapsed;


                // Get the item name column
                column = dgProducts.Columns[1];
                column.Header = "שם";

                // Get the amount column
                //column = dgProducts.Columns[1];
                //column.Visibility = System.Windows.Visibility.Collapsed;

                // Get the icon column
                //column = dgProducts.Columns[2];
                //column.Header = "אייקון";

                // Get the price column
                column = dgProducts.Columns[2];
                column.Header = "מחיר";

                // Get the costume column
                column = dgProducts.Columns[3];
                column.Visibility = System.Windows.Visibility.Collapsed;

                // Get the costume column
                column = dgProducts.Columns[4];
                column.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void AddImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ucPricePerMin ucPricePerMin = new ucPricePerMin();
            spPrices.Children.Add(ucPricePerMin);
        }

        private void SetPricePerMin()
        {
            foreach (KeyValuePair<int,double> item in 
                CommonConfig.GetInstance().PricePerMin)
            {
                ucPricePerMin ucPricePerMin = new ucPricePerMin();

                ucPricePerMin.SetMin(item.Key);
                ucPricePerMin.SetPrice((int)item.Value);

                spPrices.Children.Add(ucPricePerMin);    
            }
        }
    }
}
