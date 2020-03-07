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
using System.Text.RegularExpressions;

namespace GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for dlgInputPrice.xaml
    /// </summary>
    public partial class dlgInputPrice : Window
    {
        private bool m_bIsAccepted;

        public decimal Price
        {
            get
            {
                return (decimal)ucNumericValueAgurot.Value;
            }
        }

        public bool IsAccepted
        {
            get { return m_bIsAccepted; }
        }

        public dlgInputPrice()
        {
            InitializeComponent();

            m_bIsAccepted = false;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            m_bIsAccepted = true;

            Close();
        }

        

        
    }
}
