using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.XamForms.SignaturePad.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]


namespace Acr.XamForms.SignaturePad.iOS {

	public class SignatureService : ISignatureService {

        internal SignaturePadConfiguration CurrentConfig { get; private set; }

        public Task<SignatureResult> Request(SignaturePadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken)) {
			config = config ?? SignaturePadConfiguration.Default;
            var tcs = new TaskCompletionSource<SignatureResult>();
            var controller = new SignatureServiceController(config, x => tcs.TrySetResult(x));

            var topCtrl = this.GetTopViewController();
            topCtrl.PresentViewController(controller, true, null);
            cancelToken.Register(() => {
                tcs.TrySetCanceled();
                controller.DismissViewController(true, null);
            });
			return tcs.Task;
		}


        protected virtual UIWindow GetTopWindow() {
            return UIApplication.SharedApplication
                .Windows
                .Reverse()
                .FirstOrDefault(x =>
                    x.WindowLevel == UIWindowLevel.Normal &&
                    !x.Hidden
                );
        }


        protected virtual UIView GetTopView() {
            return this.GetTopWindow().Subviews.Last();
        }


        protected virtual UIViewController GetTopViewController() {
            var root = this.GetTopWindow().RootViewController;
            var tabs = root as UITabBarController;
            if (tabs != null)
				return tabs.PresentedViewController ?? tabs.SelectedViewController;

            var nav = root as UINavigationController;
            if (nav != null)
                return nav.VisibleViewController;

            if (root.PresentedViewController != null)
                return root.PresentedViewController;

            return root;
        }

        /// <summary>
        /// Return the Current SignaturePad Configuration
        /// </summary>
        /// <returns>CurrentConfig - The currently set signature pad configuation</returns>
        public SignaturePadConfiguration GetConfiguration()
        {
            if (CurrentConfig == null)
            {
                CurrentConfig = SignaturePadConfiguration.Default;
            }
            return CurrentConfig;
        }
    }
}