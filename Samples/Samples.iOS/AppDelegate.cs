using System;
using Foundation;
using UIKit;
using Xamarin.Forms;


namespace Samples.iOS {

    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate {
        private UIWindow window;


        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            Forms.Init();
            var startView = App.GetMainPage().CreateViewController();
            window = new UIWindow((RectangleF)UIScreen.MainScreen.Bounds) {
                RootViewController = startView
            };
            window.MakeKeyAndVisible();

            return true;
        }
    }
}
