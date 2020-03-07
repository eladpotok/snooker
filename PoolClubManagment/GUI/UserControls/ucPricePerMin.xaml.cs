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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for ucPricePerMin.xaml
    /// </summary>
    public partial class ucPricePerMin : UserControl
    {
        public int Min
        {
            get { return ucMin.Value; }
        }

        public int Price
        {
            get { return ucPrice.Value; }
        }

        public ucPricePerMin()
        {
            InitializeComponent();
        }

        public void SetMin(int nMin)
        {
            ucMin.DefaultValue = nMin;
        }

        public void SetPrice(int nPrice)
        {
            ucPrice.DefaultValue = nPrice;
        }
    }
}
