using System;
using System.Globalization;
using Xamarin.Forms;


namespace Acr.XamForms.Converters {
    
    public class EllipsisConverter : IValueConverter {

        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var text = value as string;
            if (text == null)
                text = String.Empty;

            else { 
                var size = 50;
                if (parameter is int)
                    size = (int)parameter;

                if (text.Length > size + 3)
                    text = text.Substring(0, size - 3) + "...";
            }
            return text;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
