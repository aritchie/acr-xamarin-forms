using System;
using System.Diagnostics;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class Logger : ILogger {

        public void Trace(string tag, string message) {
            Debug.WriteLine("Print something");
        }


        public void Error(string tag, string message, Exception ex) {
        }
    }
}
