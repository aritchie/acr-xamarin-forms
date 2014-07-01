using System;
using System.Collections.Generic;
using Acr.XamForms.SignaturePad.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]


namespace Acr.XamForms.SignaturePad.iOS {
    
    public class SignatureService : AbstractSignatureService {

        public override void Request(Action<SignatureResult> onResult) {
            var controller = new SignatureServiceController(this.Configuration, onResult);
            this.Show(controller);
        }


        public override void Load(IEnumerable<DrawPoint> points) {
            var controller = new SignatureServiceController(this.Configuration, points);
            this.Show(controller);
        }


        private void Show(SignatureServiceController controller) {
            // TODO:
        }
    }
}