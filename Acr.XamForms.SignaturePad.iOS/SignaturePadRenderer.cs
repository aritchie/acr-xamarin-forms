using System;
using System.ComponentModel;
using System.Linq;
using Acr.XamForms.SignaturePad.iOS;
using Acr.XamForms.SignaturePad.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using NativeView = SignaturePad.SignaturePadView;

[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.iOS {
    
    public class SignaturePadRenderer : ViewRenderer<SignaturePadView, NativeView> {


        protected override void OnElementChanged(ElementChangedEventArgs<SignaturePadView> e) {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;
                
            this.SetNativeControl(new NativeView());
            this.Element.SetInternals(
                // TODO: careful
                imgFormat => imgFormat == ImageFormatType.Jpg
                        ? this.Control.GetImage().AsJPEG().AsStream()
                        : this.Control.GetImage().AsPNG().AsStream(), 
                () => this.Control.Points.Select(x => new DrawPoint(x.X, x.Y)), 
                () => this.Control.IsBlank
            );
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName) {
                case "CaptionText":
                    this.Control.Caption.Text = this.Element.CaptionText;
                    break;

                case "CaptionTextColor":
                    this.Control.Caption.TextColor = this.Element.CaptionTextColor.ToUIColor();
                    break;

                case "ClearText":
                    this.Control.ClearLabel.TitleLabel.Text = this.Element.ClearText;
                    break;

                case "ClearTextColor":
                    this.Control.ClearLabel.TitleLabel.TextColor = this.Element.ClearTextColor.ToUIColor();
                    break;

                case "PromptText":
                    this.Control.SignaturePrompt.Text = this.Element.PromptText;
                    break;

                case "PromptTextColor":
                    this.Control.SignaturePrompt.TextColor = this.Element.PromptTextColor.ToUIColor();
                    break;

                case "SignatureLineColor":
                    this.Control.SignatureLineColor = this.Element.SignatureLineColor.ToUIColor();
                    break;

                case "StrokeColor":
                    this.Control.StrokeColor = this.Element.StrokeColor.ToUIColor();
                    break;

                case "StrokeWidth":
                    this.Control.StrokeWidth = this.Element.StrokeWidth;
                    break;
            }
        }
    }
}