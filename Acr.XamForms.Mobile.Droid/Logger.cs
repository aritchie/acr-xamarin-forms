using System;
using Android.Util;


namespace Acr.XamForms.Mobile.Droid {
    
    public class Logger : ILogger {

        public void Trace(string tag, string message) {
            Log.Debug(tag, message);
        }


        public void Error(string tag, string message, Exception ex) {
        }
    }
}