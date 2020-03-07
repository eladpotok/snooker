using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace GUI.Converters
{
    class cnvBooleamToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;
            ImageSource strResult = BitmapToBitmapSourceConverter.NewFrameHandler(
                Properties.Resources.busy);

            if (bValue)
            {
                strResult = BitmapToBitmapSourceConverter.NewFrameHandler(
                Properties.Resources.available);
            }

            return strResult;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
