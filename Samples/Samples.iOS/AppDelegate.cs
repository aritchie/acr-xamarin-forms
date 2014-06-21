using System;
using Acr.XamForms.BarCodeScanner;
using Acr.XamForms.Mobile.iOS;
using Acr.XamForms.UserDialogs.iOS;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;


namespace Samples.iOS {

    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate {
        private UIWindow window;


        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            Forms.Init();

            // HACK: fix linker issues
            new UserDialogService();
            new Settings();
            new BarCodeScanner();
            new DeviceInfo();
            var startView = App.GetMainPage().CreateViewController();
            window = new UIWindow(UIScreen.MainScreen.Bounds) {
                RootViewController = startView
            };
            window.MakeKeyAndVisible();

            return true;
        }

        //void Forms_ViewInitialized(object sender, ViewInitializedEventArgs e) {
        //}
    }
}
