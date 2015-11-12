using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Acr.XamForms.SignaturePad.Windows8
{
    public class PlatformConverters
    {
        public static Windows.UI.Color FormsToWindowsColor(Xamarin.Forms.Color color)
        {
            return Windows.UI.ColorHelper.FromArgb((byte)(color.A * 255.0), (byte)(color.R * 255.0), (byte)(color.G * 255.0), (byte)(color.B * 255.0));
        }

        public static SolidColorBrush BrushFromColor(Xamarin.Forms.Color color)
        {
            var brush = new SolidColorBrush();
            brush.Color = FormsToWindowsColor(color);
            return brush;
        }
    }
}
