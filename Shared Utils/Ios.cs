#if __IOS__
using System;
using System.Linq;
using MonoTouch.UIKit;


namespace Acr.XamForms {
    
    public static class Utils {

        public static UIView GetTopView() {
            return UIApplication.SharedApplication.KeyWindow.RootViewController.View;
            //return UIApplication.SharedApplication.Windows.Last().Subviews.Last();
        }


        public static UIViewController GetTopViewController() {
            var top = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (top.PresentedViewController != null)
                top = top.PresentedViewController;

            return top;
 /*
- (UIViewController*)topViewController {
    return [self topViewControllerWithRootViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
}

- (UIViewController*)topViewControllerWithRootViewController:(UIViewController*)rootViewController {
    if ([rootViewController isKindOfClass:[UITabBarController class]]) {
        UITabBarController* tabBarController = (UITabBarController*)rootViewController;
        return [self topViewControllerWithRootViewController:tabBarController.selectedViewController];
    } else if ([rootViewController isKindOfClass:[UINavigationController class]]) {
        UINavigationController* navigationController = (UINavigationController*)rootViewController;
        return [self topViewControllerWithRootViewController:navigationController.visibleViewController];
    } else if (rootViewController.presentedViewController) {
        UIViewController* presentedViewController = rootViewController.presentedViewController;
        return [self topViewControllerWithRootViewController:presentedViewController];
    } else {
        return rootViewController;
    }
}*/           
        }


        public static void Dispatch(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}
#endif