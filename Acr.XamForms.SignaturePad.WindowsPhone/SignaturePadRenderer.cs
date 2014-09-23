using System;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.SignaturePad.WindowsPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;


[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.WindowsPhone {
    
    public class SignaturePadRenderer : ViewRenderer<SignaturePadView, global::Xamarin.Controls.SignaturePad> {

    }
}