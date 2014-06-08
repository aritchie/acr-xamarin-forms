using System;
using System.Threading;
using Android.App;


namespace Acr.XamForms.UserDialogs.Droid {
    
    public static class Droid {

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
    }
}