using System;


namespace Acr.XamForms.Mobile.iOS {
    
    public class Logger : ILogger {

        public void Trace(string tag, string message) {
            Console.WriteLine("");
        }


        public void Error(string tag, string message, Exception ex) {
        }
    }
}