using System;
using Acr.XamForms.Mobile.Droid;
using Android.Util;
using Xamarin.Forms;


[assembly: Dependency(typeof(Logger))]

namespace Acr.XamForms.Mobile.Droid {
    
    public class Logger : AbstractLogger {

        protected override void NativeDebug(string tag, string message) {
            Log.Debug(tag, message);
        }


        protected override void NativeError(string tag, string message) {
            Log.Error(tag, message);
        }
    }
}