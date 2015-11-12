using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.XamForms.SignaturePad.Windows8;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Core;

[assembly: Dependency(typeof(Acr.XamForms.SignaturePad.Windows8.SignatureService))]


namespace Acr.XamForms.SignaturePad.Windows8 {

    public class SignatureService : ISignatureService {

		internal SignaturePadConfiguration CurrentConfig { get; private set; }
		private TaskCompletionSource<SignatureResult> tcs;


		internal void Complete(SignatureResult result) {
			this.tcs.TrySetResult(result);
		}


		internal void Cancel() {
			this.tcs.TrySetResult(new SignatureResult(true, null, null));
		}


		public virtual Task<SignatureResult> Request(SignaturePadConfiguration config, CancellationToken cancelToken) {

            CurrentConfig = config ?? SignaturePadConfiguration.Default;

			this.tcs = new TaskCompletionSource<SignatureResult>();
			cancelToken.Register(this.Cancel);


            
            //Navigate to the SignatureServicePate
            ((Windows.UI.Xaml.Controls.Frame)Window.Current.Content).Navigate(typeof(SignaturePadServicePage));
           
            return this.tcs.Task;
		}
    }
}