using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


namespace Samples.iOS {

    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        private UIWindow window;


        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            Forms.Init();
          
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
