#if __IOS__
using System;
using System.Linq;
using MonoTouch.UIKit;


namespace Acr.XamForms {
    
    public static class Utils {

        public static UIView GetTopView() {
            return UIApplication.SharedApplication.KeyWindow.Subviews.Last();
        }


        public static UIViewController GetTopViewController() {
            var root = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var tabs = root as UITabBarController;
            if (tabs != null)
                return tabs.SelectedViewController;

            var nav = root as UINavigationController;
            if (nav != null)
                return nav.VisibleViewController;

            if (root.PresentedViewController != null)
                return root.PresentedViewController;

            return root;        
        }


        public static void Dispatch(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}
#endif