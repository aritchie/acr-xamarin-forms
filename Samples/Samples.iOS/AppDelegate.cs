using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;


namespace Samples.iOS {

    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate {
        private UIWindow window;


        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            Forms.Init();

            var startView = App.GetMainPage().CreateViewController();
            window = new UIWindow(UIScreen.MainScreen.Bounds) {
                RootViewController = startView
            };
            window.MakeKeyAndVisible();

            return true;
        }
    }
}
