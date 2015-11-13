using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.XamForms.SignaturePad.Windows8;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;

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

            var frame = ((Windows.UI.Xaml.Controls.Frame)Window.Current.Content);
            //Navigate to the SignatureServicePage
            frame.Navigate(typeof(SignaturePadServicePage));

            return this.tcs.Task;
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