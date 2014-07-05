using System;
using Acr.XamForms.SignaturePad.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]


namespace Acr.XamForms.SignaturePad.Droid {
    
    public class SignatureService : AbstractSignatureService {

        //internal static IEnumerable<DrawPoint> CurrentPoints { get; private set; }
        internal static SignaturePadConfiguration CurrentConfig { get; private set; }
        internal static Action<SignatureResult> OnResult { get; private set; }


        public override void Request(Action<SignatureResult> onResult) { 
            //CurrentPoints = null;
            CurrentConfig = this.Configuration;
            OnResult = onResult;
            Forms.Context.StartActivity(typeof(SignatureServiceActivity));
        }


        //public override void Load(IEnumerable<DrawPoint> points) {
        //    CurrentConfig = this.Configuration;
        //    CurrentPoints = points;
        //    OnResult = null;
        //}
    }
}