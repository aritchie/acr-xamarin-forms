using System;
using System.Threading;
using Android.App;
using Android.Content;
using Xamarin.Forms;


namespace Acr.XamForms.UserDialogs.Droid {
    
    public static class Utils {
        
        public static void RequestMainThread(Action action) {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
                action();
            else
                Application.SynchronizationContext.Post(x => MaskException(action), null);
        }


        public static void MaskException(Action action) {
            try {
                action();
            }
            catch { }
        }


        public static Context GetActivityContext() {
            return Forms.Context;
        }
    }
}