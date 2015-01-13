using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.XamForms.SignaturePad.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]


namespace Acr.XamForms.SignaturePad.iOS {

	public class SignatureService : ISignatureService {

		public Task<SignatureResult> Request(SignaturePadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken)) {
			config = config ?? SignaturePadConfiguration.Default;
            var tcs = new TaskCompletionSource<SignatureResult>();
            var controller = new SignatureServiceController(config, x => tcs.TrySetResult(x));

            var topCtrl = Utils.GetTopViewController();
            topCtrl.PresentViewController(controller, true, null);
            cancelToken.Register(() => {
                tcs.TrySetCanceled();
                controller.DismissViewController(true, null);
            });
			return tcs.Task;
		}
	}
}