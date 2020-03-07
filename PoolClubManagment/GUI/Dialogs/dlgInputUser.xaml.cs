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

namespace GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for dlgInputUser.xaml
    /// </summary>
    public partial class dlgInputUser : Window
    {
        private string m_strNameValue;

        public string NameValue
        {
            get { return m_strNameValue; }
            set { m_strNameValue = value; }
        }

        public dlgInputUser()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            m_strNameValue = txbName.Text;

            Close();
        }

        private void txbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAccept_Click(null, null);
            }
        }
    }
}
