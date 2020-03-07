using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace GUI.Converters
{
    class cnvItemToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Common.Item item = value as Common.Item;
            Visibility vResult = Visibility.Collapsed;

            if (item != null &&
                !string.IsNullOrEmpty(item.Name))
            {
                vResult = Visibility.Visible;
            }

            return vResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
