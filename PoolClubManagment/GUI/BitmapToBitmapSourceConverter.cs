using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GUI
{
    public static class BitmapToBitmapSourceConverter
    {
        public static BitmapSource NewFrameHandler(Bitmap bitmap)
        {
            BitmapSource bitSrc = null;
            var hBitmap = bitmap.GetHbitmap();

            bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            return bitSrc;
        }
    }
}
