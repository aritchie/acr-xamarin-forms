using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;


namespace Acr.XamForms.Converters {
    
    public class BlobToImageSourceConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Stream stream = null;

            var bytes = value as byte[];
            if (bytes != null)
                stream = new MemoryStream(bytes);
            else 
                stream = value as Stream;

            if (stream == null)
                throw new ArgumentException("Blob not found");

            return ImageSource.FromStream(() => stream);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
