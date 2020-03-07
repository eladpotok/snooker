using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Common;

namespace GUI.Converters
{
    class cnvListOfItemsToTotalPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<ItemsToOrder> items = (ObservableCollection<ItemsToOrder>)value;

            decimal fPrice = 0;
            

            foreach (ItemsToOrder item in items)
            {
                fPrice += item.Item.Price * item.Amount;
            }

            string strPrice = fPrice.ToString();
            string[] prices = strPrice.Split('.');

            if (prices.Length > 1)
            {
                strPrice = prices[0] + "." + prices[1][0] + prices[1][1];
            }


            return strPrice;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
