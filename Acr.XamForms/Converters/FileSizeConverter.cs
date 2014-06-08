//using System;
//using System.Collections.Generic;
//using System.Globalization;

//namespace Acr.XamForms.Converters {
    
//    public class FileSizeConverter : MvxValueConverter<long> {
//        private static readonly List<string> suffixes = new List<string> { "bytes", "KB", "MB", "GB", "TB" };


//         TODO: multilingual on types above?
//        protected override object Convert(long fileSize, Type targetType, object parameter, CultureInfo culture) {
//            var pow = Math.Floor((fileSize > 0 ? Math.Log(fileSize) : 0) / Math.Log(1024));
//            pow = Math.Min(pow, suffixes.Count - 1);
//            var value = fileSize / Math.Pow(1024, pow);
//            return value.ToString(pow == 0 ? "F0" : "F2") + " " + suffixes[(int)pow];
//        }
//    }
//}
//using System;
//using System.Globalization;
//using Xamarin.Forms;

//namespace Crosschat.Client.Views.ValueConverters
//{
//    public class InverterConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (value is bool)
//                return !((bool) value);
//            return false;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotSupportedException();
//        }
//    }
//}