using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Common;
using BL;

namespace GUI.Converters
{
    class cnvProductsAndGameTimeToTotalPriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dPrice = 0;

            if (values[0] != null &&
                values[1] != null)
            {
                ObservableCollection<ItemsToOrder> items = 
                    (ObservableCollection<ItemsToOrder>)values[0];

                foreach (ItemsToOrder item in items)
                {
                    dPrice += (double)(item.Item.Price * item.Amount);
                }

                Dictionary<int, double> pricePerMin = SettingsXmlProvider.ReadPriceGame();

                pricePerMin.OrderBy(t => t.Key);
            }

            return dPrice;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
