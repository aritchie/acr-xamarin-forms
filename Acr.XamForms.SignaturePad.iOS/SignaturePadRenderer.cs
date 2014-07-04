using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.SignaturePad.iOS;
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
                imgFormat => imgFormat == ImageFormatType.Jpg
                        ? this.Control.GetImage().AsJPEG().AsStream()
                        : this.Control.GetImage().AsPNG().AsStream(),
                () => this.Control.Points.Select(x => new DrawPoint(x.X, x.Y)), 
                x => this.Control.LoadPoints(x.Select(y => new PointF(y.X, y.Y)).ToArray()),
                () => this.Control.IsBlank
            );

            var el = e.NewElement;

            this.Control.Caption.Text = el.CaptionText;
            this.Control.Caption.TextColor = el.CaptionTextColor.ToUIColor();
            this.Control.ClearLabel.TitleLabel.Text = el.ClearText;
            this.Control.ClearLabel.TitleLabel.TextColor = el.ClearTextColor.ToUIColor();
            this.Control.SignaturePrompt.Text = el.PromptText;
            this.Control.SignaturePrompt.TextColor = el.PromptTextColor.ToUIColor();
            this.Control.SignatureLineColor = el.SignatureLineColor.ToUIColor();
            this.Control.StrokeColor = el.StrokeColor.ToUIColor();
            this.Control.StrokeWidth = el.StrokeWidth;

        }
    }
}