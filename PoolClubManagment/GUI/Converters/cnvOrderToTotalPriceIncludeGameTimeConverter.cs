using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Common;

namespace GUI.Converters
{
    class cnvOrderToTotalPriceIncludeGameTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            float price = 0;

            if (value is Order)
            {
                Order order = (Order)value;

                price = order.TotalPrice;

                Dictionary<int, double> minPerPrice =
                    new Dictionary<int, double>(CommonConfig.GetInstance().PricePerMin);

                // Sort the dictionary by descending
                minPerPrice = minPerPrice.OrderByDescending(t => t.Key).
                    ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

                int nNumberrOfMin = order.GameTime.Minute;

                while (nNumberrOfMin > 
                    minPerPrice.Keys.ToList()[minPerPrice.Keys.Count-1])
                {
                    foreach (KeyValuePair<int, double> minPerPriceItem in minPerPrice)
                    {
                        if (nNumberrOfMin >= minPerPriceItem.Key)
                        {
                            price += (float)minPerPriceItem.Value;
                            nNumberrOfMin -= minPerPriceItem.Key;
                            break;
                        }
                    }
                }
            }

            return price;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
