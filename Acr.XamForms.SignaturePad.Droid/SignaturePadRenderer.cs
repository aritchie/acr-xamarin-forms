using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.SignaturePad.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NativeView = SignaturePad.SignaturePadView;

[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.Droid {
    
    public class SignaturePadRenderer : ViewRenderer<SignaturePadView, NativeView> {

        protected override void OnElementChanged(ElementChangedEventArgs<SignaturePadView> e) {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;
                
            this.SetNativeControl(new NativeView(Forms.Context));
            this.Element.SetInternals(
                imgFormat => {
                    using (var image = this.Control.GetImage()) {
                        var stream = new MemoryStream();
                        var format = imgFormat == ImageFormatType.Png
                            ? Android.Graphics.Bitmap.CompressFormat.Png
                            : Android.Graphics.Bitmap.CompressFormat.Jpeg;
                        image.Compress(format, 100, stream); // TODO: quality control
                        return stream; // TODO: careful
                    }
                }, 
                () => this.Control.Points.Select(x => new DrawPoint(x.X, x.Y)), 
                x => this.Control.LoadPoints(x.Select(y => new PointF(y.X, y.Y)).ToArray()),
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
                    this.Control.Caption.SetTextColor(this.Element.CaptionTextColor.ToAndroid());
                    break;

                case "ClearText":
                    this.Control.ClearLabel.Text = this.Element.ClearText;
                    break;

                case "ClearTextColor":
                    this.Control.ClearLabel.SetTextColor(this.Element.ClearTextColor.ToAndroid());
                    break;

                case "PromptText":
                    this.Control.SignaturePrompt.Text = this.Element.PromptText;
                    break;

                case "PromptTextColor":
                    this.Control.SignaturePrompt.SetTextColor(this.Element.PromptTextColor.ToAndroid());
                    break;

                case "SignatureLineColor":
                    this.Control.SignatureLineColor = this.Element.SignatureLineColor.ToAndroid();
                    break;

                case "StrokeColor":
                    this.Control.StrokeColor = this.Element.StrokeColor.ToAndroid();
                    break;

                case "StrokeWidth":
                    this.Control.StrokeWidth = this.Element.StrokeWidth;
                    break;
            }
        }
    }
}