using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;


namespace Acr.XamForms.Converters {

    public class FileSizeConverter : IValueConverter {
        // TODO: globalization
        private static readonly List<string> suffixes = new List<string> { "bytes", "KB", "MB", "GB", "TB" };
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return 0;

            if (!(value is long) && !(value is int))
                throw new ArgumentException("Invalid data type - " + value.GetType());

            var fileSize = System.Convert.ToInt64(value);
            var pow = Math.Floor((fileSize > 0 ? Math.Log(fileSize) : 0) / Math.Log(1024));
            pow = Math.Min(pow, suffixes.Count - 1);
            var result = fileSize / Math.Pow(1024, pow);
            return result.ToString(pow == 0 ? "F0" : "F2") + " " + suffixes[(int)pow];
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException("FileSize conversion is one-way");
        }
    }
}