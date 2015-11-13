using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.XamForms.SignaturePad.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]


namespace Acr.XamForms.SignaturePad.Droid {

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
			Forms.Context.StartActivity(typeof(SignaturePadActivity));

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