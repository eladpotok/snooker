using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GUI.Converters
{
    class cnvAmountToTotalPriceMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal dTotal = 0;
            if (values[0] != null &&
                values[1] != null)
            {

                int nAmount = int.Parse(values[0].ToString());
                decimal nPrice = decimal.Parse(values[1].ToString());

                dTotal = (decimal)(nAmount * nPrice);
            }

            string strPrice = dTotal.ToString();
            string[] prices = strPrice.Split('.');

            if (prices.Length > 1)
            {
                strPrice = prices[0] + "." + prices[1][0] + prices[1][1];
            }

            return strPrice;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
