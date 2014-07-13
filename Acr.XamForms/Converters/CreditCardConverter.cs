// CREDIT TO GREG SHACKLES
// http://www.gregshackles.com/2013/11/auto-formatting-text-inputs-with-mvvmcross-and-value-converters/
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;


namespace Acr.XamForms.Converters {

    public class CreditCardConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return String.Empty;

            var builder = new StringBuilder(Regex.Replace(value.ToString(), @"\D", ""));
            foreach (var i in Enumerable.Range(0, builder.Length / 4).Reverse())
                builder.Insert(4 * i + 4, " ");

            return builder.ToString().Trim();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null
                ? String.Empty
                : Regex.Replace(value.ToString(), @"\D", "");
        }
    }
}
