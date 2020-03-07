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
using GUI.UserControls;
using GUI.Dialogs;
using System.IO;
using System.Windows.Media.Animation;
using DAL;
using Common;
using BL;
using Microsoft.Win32;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Data Members

        private Point   m_pStartPoint;
        private Point   m_pPointInRectangle;
        private bool    m_bMouseClickOnObject;
        private bool    m_bInMove;
        private ucPlace m_ucDraggedObject;
        
        #endregion

        #region Ctor

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                m_bMouseClickOnObject = false;

                InitializeLogo();

                // Load all items from the db
                ItemsCollectionProvider.GetInstance().LoadItemsFromDB();
                
                // Read and save the prices for the game time
                CommonConfig.GetInstance().SetPricePerMin(
                    SettingsXmlProvider.ReadPriceGame());
            }
            catch (Exception)
            {
                
            }
            
        }

        #endregion

        #region Drag N Drop

        void ucChairBar_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                //Check if it's not the correct object
                if (!e.Data.GetDataPresent("Place") || sender == e.Source)
                {
                    e.Effects = DragDropEffects.Copy;
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        void ucChairBar_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //Get the mouse position propentional to the screen
                Point mousePos = e.GetPosition(null);

                //Get the difference of the point
                Vector diff = m_pStartPoint - mousePos;

                //Check if the user clicked on the mouse left button 
                if (e.LeftButton == MouseButtonState.Pressed &&
                    m_bMouseClickOnObject)
                {
                    // Check if the movement is enouth for drag movement
                    if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                    {
                        // Show and active animation for garbage
                        brdGarbage.Visibility = System.Windows.Visibility.Visible;
                        Storyboard storyBoard = Resources["stbGarbage"] as Storyboard;
                        storyBoard.Begin();

                        ucPlace draggedItem = sender as ucPlace;

                        // Set boolean to alert that the object is on move
                        m_bInMove = true;

                        // Save the object as a member
                        m_ucDraggedObject = draggedItem;

                        // Change the opacity 
                        draggedItem.Opacity = 0.4;

                        // Create a drag object
                        DataObject dragData = new DataObject("Place", draggedItem);
                        DragDropEffects ddEffect = DragDrop.DoDragDrop(draggedItem, dragData, DragDropEffects.Move);
                    }
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        void ucPlace_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ucPlace ucPlace = sender as ucPlace;

                // Save the first click point
                m_pStartPoint = e.GetPosition(null);

                // Save the point propertional to the user control
                m_pPointInRectangle = e.GetPosition(ucPlace);

                m_bMouseClickOnObject = true;
            }
            catch (Exception)
            {
                
            }
        }

        private void cnvPool_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent("Place"))
                {
                    m_bInMove = false;
                    m_ucDraggedObject.Opacity = 1;

                    // Check if the boject is not out of bound
                    if (IsOutOfBound())
                    {
                        // Set the correct bounds
                        SetBounds();
                    }

                    brdGarbage.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        private void cnvPool_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (m_bInMove)
                {
                    Vector v = e.GetPosition(cnvPool) - m_pPointInRectangle;

                    Canvas.SetLeft(m_ucDraggedObject, v.X);
                    Canvas.SetTop(m_ucDraggedObject, v.Y);
                    Canvas.SetRight(m_ucDraggedObject, cnvPool.ActualWidth -
                        (Canvas.GetLeft(m_ucDraggedObject) + m_ucDraggedObject.ActualWidth));
                    Canvas.SetBottom(m_ucDraggedObject, cnvPool.ActualHeight -
                        (Canvas.GetTop(m_ucDraggedObject) + m_ucDraggedObject.ActualHeight));
                }
            }
            catch (Exception)
            {
              
            }
            
        }

        #endregion

        #region Events Handler

        private void miAddPlace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem miItem = sender as MenuItem;

                // Check the type of the boject
                string strType = miItem.Tag.ToString();

                AddPlaceToCanvas(strType, new Point(0, 0));
            }
            catch (Exception)
            {
                
            }
      
        }

        private void AddPlaceToCanvas(string strType, Point point)
        {
            try
            {
                ucPlace ucPlace = null;

                // Create object by the type
                switch (strType)
                {
                    case "BarChair":
                        ucPlace = new ucPlace(BitmapToBitmapSourceConverter.NewFrameHandler(
                            Properties.Resources.bar_chair), false, PresentationObject.BAR_CHAIR);
                        break;
                    case "BarTable":
                        ucPlace = new ucPlace(BitmapToBitmapSourceConverter.NewFrameHandler(
                            Properties.Resources.bar_table), false, PresentationObject.BAR_TABLE,
                            false);
                        break;
                    case "PoolTable":
                        ucPlace = new ucPlace(BitmapToBitmapSourceConverter.NewFrameHandler(
                            Properties.Resources.pool_table), true, PresentationObject.POOL_TABLE);
                        break;
                    default:
                        break;
                }

                Canvas.SetLeft(ucPlace, point.X);
                Canvas.SetTop(ucPlace, point.Y);

                // Register all events
                ucPlace.PreviewMouseLeftButtonDown += ucPlace_PreviewMouseLeftButtonDown;
                ucPlace.PreviewMouseMove += ucChairBar_PreviewMouseMove;
                ucPlace.DragEnter += ucChairBar_DragEnter;
                ucPlace.MouseDoubleClick += ucPlace_MouseDoubleClick;
                ucPlace.MouseLeave += ucPlace_MouseLeave;

                // Add the usercontrol to the canvas
                cnvPool.Children.Add(ucPlace);
            }
            catch (Exception)
            {
                
            }
            
        }

        private void miAddProducts_Click(object sender, RoutedEventArgs e)
        {
            dlgAddProducts dlgAddProducts = new dlgAddProducts();
            dlgAddProducts.ShowDialog();
        }

        private void ucPlace_MouseLeave(object sender, MouseEventArgs e)
        {
            m_bMouseClickOnObject = false;
        }

        private void ucPlace_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ucPlace ucPlace = sender as ucPlace;

            if (ucPlace != null &&
                ucPlace.IsSittingPlace)
            {
                // Check if the place is available
                if (ucPlace.DataContext == null)
                {
                    ucPlace.OpenCheck();
                }
                else
                {
                    // Check if the place is available
                    if (ucPlace.IsAvailable)
                    {
                        ucPlace.OpenCheck();
                    }
                }

                // Create and open dialog which contains the check bill
                dlgOrderCheck orderCheck = new dlgOrderCheck();

                orderCheck.OnOrderMoved += orderCheck_OnOrderMoved;

                orderCheck.DataContext = ucPlace.DataContext;
                orderCheck.ShowDialog();
            }
        }

        private void orderCheck_OnOrderMoved(Order order)
        {
            ucPlace MovedPlace = null;

            foreach (ucPlace item in cnvPool.Children)
            {
                if (item.DataContext == order)
                {
                    MovedPlace = item;
                    break;
                }
            }

            dlgPlacesList dlg = new dlgPlacesList(cnvPool.Children, MovedPlace);

            dlg.ShowDialog();

            if (dlg.IsAccpeted)
            {
                if (dlg.ChosenPlace != null)
                {
                    if (dlg.ChosenPlace.IsAvailable)
                    {
                        if (Verify("האם אתה בטוח שאתה רוצה להעביר את ההזמנה לשולחן אחר?"))
                        {
                            dlg.ChosenPlace.MoveDataContext(order);

                            if (MovedPlace != null)
                            {
                                MovedPlace.OpenCheck();
                            }
                        }
                    }
                    else
                    {
                        if (Verify("האם אתה בטוח שאתה למזג בין ההזמנות?"))
                        {
                            dlg.ChosenPlace.MergeDataContext(order);

                            if (MovedPlace != null)
                            {
                                MovedPlace.OpenCheck();
                            }
                        }
                    }
                }
            }
        }

        void storyBoard_Completed(object sender, EventArgs e)
        {
            txbLogo.Visibility = System.Windows.Visibility.Collapsed;
        } 

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if the dragged object is out of bound
        /// </summary>
        /// <returns></returns>
        private bool IsOutOfBound()
        {
            // Get the margin of the object
            double dLeft = Canvas.GetLeft(m_ucDraggedObject);
            double dRight = Canvas.GetRight(m_ucDraggedObject);
            double dTop = Canvas.GetTop(m_ucDraggedObject);
            double dBottom = Canvas.GetBottom(m_ucDraggedObject);

            return dLeft < 0 || dRight < 0
                || dTop < 0 || dBottom < 0;
        }

        /// <summary>
        /// Set the correct bound in case that the boject is deviated from the bound
        /// </summary>
        private void SetBounds()
        {
            // Get the margin of the object
            double dLeft = Canvas.GetLeft(m_ucDraggedObject);
            double dRight = Canvas.GetRight(m_ucDraggedObject);
            double dTop = Canvas.GetTop(m_ucDraggedObject);
            double dBottom = Canvas.GetBottom(m_ucDraggedObject);

            if (dLeft < 0)
            {
                Canvas.SetLeft(m_ucDraggedObject, 0);
            }
            if (dTop < 0)
            {
                Canvas.SetTop(m_ucDraggedObject, 0);
            }
            if (dRight < 0)
            {
                Canvas.SetLeft(m_ucDraggedObject, 
                    cnvPool.ActualWidth - m_ucDraggedObject.ActualWidth);
            }
            if (dBottom < 0)
            {
                Canvas.SetTop(m_ucDraggedObject,
                    cnvPool.ActualHeight - m_ucDraggedObject.ActualHeight);
            }
        }

        /// <summary>
        /// Create and turn on the logo of the software
        /// </summary>
        private void InitializeLogo()
        {
            Storyboard storyBoard = Resources["stbLogo"] as Storyboard;
            storyBoard.Completed += new EventHandler(storyBoard_Completed);
            storyBoard.Begin();
        }

        #endregion

        private void miOrderReport_Click(object sender, RoutedEventArgs e)
        {
            dlgOrders dlg = new dlgOrders();
            dlg.ShowDialog();
        }

        private void miLoadMap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofdDialog = new OpenFileDialog();

                ucPlace.m_nIndexTable = 0;

                ofdDialog.Filter = "XML Files (.xml)|*.xml";

                if ((bool)ofdDialog.ShowDialog())
                {
                    cnvPool.Children.Clear();

                    PresentationDetails pdDetails =
                        PresenetationProvider.ReadPresentation(ofdDialog.FileName);

                    foreach (KeyValuePair<Point, ObjectDetails> pair in pdDetails.dicObject)
                    {
                        switch (pair.Value.poType)
                        {
                            case PresentationObject.POOL_TABLE:
                                AddPlaceToCanvas("PoolTable", pair.Key);
                                break;
                            case PresentationObject.BAR_CHAIR:
                                AddPlaceToCanvas("BarChair", pair.Key);
                                break;
                            case PresentationObject.BAR_TABLE:
                                AddPlaceToCanvas("BarTable", pair.Key);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void miSaveMap_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfdDialog = new SaveFileDialog();

            sfdDialog.Filter = "XML Files (.xml)|*.xml";

            Dictionary<Point, ObjectDetails> dic =
                new Dictionary<Point, ObjectDetails>();

            if ((bool)sfdDialog.ShowDialog())
            {
                foreach (ucPlace place in cnvPool.Children)
                {
                    Point point = new Point(Canvas.GetLeft(place),
                        Canvas.GetTop(place));

                    dic.Add(point, new ObjectDetails(place.PlaceName, place.PlaceType));
                }

                PresenetationProvider.SavePresentation(sfdDialog.FileName, dic, ucPlace.m_nIndexTable);
            }
        }

        private void brdGarbage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Place"))
            {
                m_bInMove = false;
                cnvPool.Children.Remove(m_ucDraggedObject);
                m_ucDraggedObject = null;

                brdGarbage.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private bool Verify(string strMessage)
        {
            return MessageBox.Show(strMessage, "אישור",
                MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.OK,
                MessageBoxOptions.RtlReading) == MessageBoxResult.Yes;
        }
    }
}

