using System;
using System.Collections.Generic;
using Acr.XamForms.SignaturePad;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]

namespace Acr.XamForms.SignaturePad {

    public class SignatureService : ISignatureService {
        public SignaturePadConfiguration Configuration { get; private set; }


        public SignatureService() {
            this.Configuration = new SignaturePadConfiguration {
                ImageType = ImageFormatType.Png,
                BackgroundColor = Color.White,
                CaptionTextColor = Color.Black,
                ClearTextColor = Color.Black,
                PromptTextColor = Color.White,
                StrokeColor = Color.Black,
                StrokeWidth = 2f,
                SignatureLineColor = Color.Black,

                SaveText = "Save",
                CancelText = "Cancel",
                ClearText = "Clear",
                PromptText = "",
                CaptionText = "Please Sign Here"
            };
        }



        public void LoadSignature(IEnumerable<DrawPoint> points) {
            
        }


        public virtual void RequestSignature(Action<SignatureResult> onResult) {

        }
    }
}

