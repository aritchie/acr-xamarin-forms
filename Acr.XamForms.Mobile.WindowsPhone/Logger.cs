using System;
using Acr.XamForms.Mobile.WindowsPhone;
using Xamarin.Forms;

[assembly: Dependency(typeof(Logger))]


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class Logger : AbstractLogger {
        
        protected override void NativeDebug(string tag, string message) {
            Console.WriteLine("DEBUG: {0} - {1}", tag, message);
        }


        protected override void NativeError(string tag, string message) {
            Console.WriteLine("ERROR: {0} - {1}", tag, message);
        }
    }
}
