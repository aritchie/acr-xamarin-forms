using System;
using System.Globalization;
using Xamarin.Forms;


namespace Acr.XamForms.Converters {
    
    public class ImageBytesConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var bytes = value as byte[];

            return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
