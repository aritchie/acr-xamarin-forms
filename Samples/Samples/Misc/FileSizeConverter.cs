using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Samples.Misc
{
    class FileSizeConverter: IValueConverter
    {
        private static readonly List<string> suffixes = new List<string> { "bytes", "KB", "MB", "GB", "TB" };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            long fileSize = (long)value;

            var pow = Math.Floor((fileSize > 0 ? Math.Log(fileSize) : 0) / Math.Log(1024));
            pow = Math.Min(pow, suffixes.Count - 1);
            var returnValue = fileSize / Math.Pow(1024, pow);
            return returnValue.ToString(pow == 0 ? "F0" : "F2") + " " + suffixes[(int)pow];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
