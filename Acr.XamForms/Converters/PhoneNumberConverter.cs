// CREDIT TO GREG SHACKLES
// http://www.gregshackles.com/2013/11/auto-formatting-text-inputs-with-mvvmcross-and-value-converters/
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;


namespace Acr.XamForms.Converters {
    
    public class PhoneNumberConverter : IValueConverter {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return String.Empty;

            var numbers = Regex.Replace(value.ToString(), @"\D", "");
            if (numbers.Length <= 3)
                return numbers;
            
            if (numbers.Length <= 7)
                return string.Format("{0}-{1}", numbers.Substring(0, 3), numbers.Substring(3));

            return String.Format(
                "({0}) {1}-{2}", 
                numbers.Substring(0, 3), 
                numbers.Substring(3, 3), 
                numbers.Substring(6)
            );
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null
                ? String.Empty
                : Regex.Replace(value.ToString(), @"\D", "");
        }
    }
}