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
using System.Text.RegularExpressions;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for ucNumericTextBox.xaml
    /// </summary>
    public partial class ucNumericTextBox : UserControl
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public int Value
        {
            get { return GetValueFromTextBox() ; }
        }

        public int DefaultValue
        {
            get { return (int)GetValue(DefaultValueProperty); }
            set { SetValue(DefaultValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.Register("DefaultValue", typeof(int), typeof(ucNumericTextBox), new UIPropertyMetadata(0));

        //private int m_nDefaultValue;

        //public int DefaultValue
        //{
        //    get { return m_nDefaultValue; }
        //    set { m_nDefaultValue = value; txbValue.Text = value.ToString(); }
        //}

        private int GetValueFromTextBox()
        {
            int nValue = 0;
            int.TryParse(txbValue.Text, out nValue);
            return nValue;
        }

        public ucNumericTextBox()
        {
            MinValue = int.MinValue;
            MaxValue = int.MaxValue;

            InitializeComponent();

            txbValue.Text = "0";
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]");

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            int nValue = int.Parse(txbValue.Text);
            if (nValue > MinValue)
            {
                nValue--;
                txbValue.Text = nValue.ToString();
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            int nValue = int.Parse(txbValue.Text);

            if (nValue < MaxValue)
            {
                nValue++;
                txbValue.Text = nValue.ToString();
            }
        }

        private void txbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            int nValue = 0;

            if(int.TryParse(txbValue.Text, out nValue))
            {
                if (nValue > MaxValue)
                {
                    txbValue.Text = MaxValue.ToString();
                }
            }
        }
    }
}
