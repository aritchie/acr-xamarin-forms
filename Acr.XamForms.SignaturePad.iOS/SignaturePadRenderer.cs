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

            if (e.OldElement != null || this.Element == null)
                return;

            var view = new NativeView();
            var el = e.NewElement;
            
            if (!String.IsNullOrWhiteSpace(el.CaptionText))
                view.Caption.Text = el.CaptionText;

            if (el.CaptionTextColor != Color.Default)
                view.Caption.TextColor = el.CaptionTextColor.ToUIColor();

            if (!String.IsNullOrWhiteSpace(el.ClearText))
                view.ClearLabel.TitleLabel.Text = el.ClearText;

            if (el.ClearTextColor != Color.Default)
                view.ClearLabel.TitleLabel.TextColor = el.ClearTextColor.ToUIColor();

            if (!String.IsNullOrWhiteSpace(el.PromptText))
                view.SignaturePrompt.Text = el.PromptText;

            if (el.PromptTextColor != Color.Default)
                view.SignaturePrompt.TextColor = el.PromptTextColor.ToUIColor();

            if (el.SignatureLineColor != Color.Default)
                view.SignatureLineColor = el.SignatureLineColor.ToUIColor();

            if (el.StrokeColor != Color.Default)
                view.StrokeColor = el.StrokeColor.ToUIColor();

            if (el.StrokeWidth > 0)
                view.StrokeWidth = el.StrokeWidth;
            
            this.Element.SetInternals(
                imgFormat => imgFormat == ImageFormatType.Jpg
                        ? view.GetImage().AsJPEG().AsStream()
                        : view.GetImage().AsPNG().AsStream(),
                () => view.Points.Select(x => new DrawPoint(x.X, x.Y)), 
                x => view.LoadPoints(x.Select(y => new PointF(y.X, y.Y)).ToArray()),
                () => view.IsBlank
            );

            this.SetNativeControl(view);
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            
            if (this.Element == null || this.Control == null)
                return;

            var el = this.Element;
            if (e.PropertyName == SignaturePadView.CaptionTextProperty.PropertyName)
                this.Control.Caption.Text = el.CaptionText;

            else if (e.PropertyName == SignaturePadView.CaptionTextColorProperty.PropertyName)
                this.Control.Caption.TextColor = el.CaptionTextColor.ToUIColor();

            else if (e.PropertyName == SignaturePadView.ClearTextProperty.PropertyName)
                this.Control.ClearLabel.TitleLabel.Text = el.ClearText;

            else if (e.PropertyName == SignaturePadView.ClearTextColorProperty.PropertyName)
                this.Control.ClearLabel.TitleLabel.TextColor = el.ClearTextColor.ToUIColor();

            else if (e.PropertyName == SignaturePadView.PromptTextProperty.PropertyName)
                this.Control.SignaturePrompt.Text = el.PromptText;

            else if (e.PropertyName == SignaturePadView.PromptTextColorProperty.PropertyName)
                this.Control.SignaturePrompt.TextColor = el.PromptTextColor.ToUIColor();

            else if (e.PropertyName == SignaturePadView.SignatureLineColorProperty.PropertyName)
                this.Control.SignatureLineColor = el.SignatureLineColor.ToUIColor();

            else if (e.PropertyName == SignaturePadView.StrokeColorProperty.PropertyName)
                this.Control.StrokeColor = el.StrokeColor.ToUIColor();

            else if (e.PropertyName == SignaturePadView.StrokeWidthProperty.PropertyName)
                this.Control.StrokeWidth = el.StrokeWidth;
        }
    }
}