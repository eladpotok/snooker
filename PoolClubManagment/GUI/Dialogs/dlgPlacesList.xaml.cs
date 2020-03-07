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
using Common;
using GUI.UserControls;

namespace GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for dlgPlacesList.xaml
    /// </summary>
    public partial class dlgPlacesList : Window
    {
        private bool m_bIsAccpeted;
        private ucPlace m_mergedOrder;

        public ucPlace ChosenPlace
        {
            get { return m_mergedOrder; }
            set { m_mergedOrder = value; }
        }

        public bool IsAccpeted
        {
            get { return m_bIsAccpeted; }
            set { m_bIsAccpeted = value; }
        }

        public dlgPlacesList(UIElementCollection ucPlaces, ucPlace movedPlace)
        {
            InitializeComponent();

            List<ucPlace> lstPlaces = new List<ucPlace>();

            foreach (object objPlace in ucPlaces)
            {
                if (((ucPlace)objPlace).PlaceType != BL.PresentationObject.BAR_TABLE)
                {
                    if((ucPlace)objPlace != movedPlace)
                    {
                       lstPlaces.Add((ucPlace)objPlace);
                    }
                }
            }

            this.DataContext = lstPlaces;
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            if (lsbOrders.SelectedItem != null)
            {
                ucPlace order = lsbOrders.SelectedItem as ucPlace;

                if (order != null)
                {
                    m_bIsAccpeted = true;

                    m_mergedOrder = order;

                    Close();
                }
            }
        }
    }
}
