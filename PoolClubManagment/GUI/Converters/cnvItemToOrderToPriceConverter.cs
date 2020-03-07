using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Common;

namespace GUI.Converters
{
    class cnvItemToOrderToPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ItemsToOrder itemsToOrder = (ItemsToOrder)value;

            return itemsToOrder.Item.Price * itemsToOrder.Amount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
