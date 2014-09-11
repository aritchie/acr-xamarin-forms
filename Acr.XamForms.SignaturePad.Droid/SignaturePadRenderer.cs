using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.SignaturePad.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using NativeView = SignaturePad.SignaturePadView;

[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.Droid {
    
    public class SignaturePadRenderer : ViewRenderer<SignaturePadView, NativeView> {

        protected override void OnElementChanged(ElementChangedEventArgs<SignaturePadView> e) {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            var view = new NativeView(Forms.Context);
            var el = e.NewElement;
            
            if (!String.IsNullOrWhiteSpace(el.CaptionText))
                view.Caption.Text = el.CaptionText;

            if (el.CaptionTextColor != Color.Default)
                view.Caption.SetTextColor(el.CaptionTextColor.ToAndroid());

            if (!String.IsNullOrWhiteSpace(el.ClearText))
                view.ClearLabel.Text = el.ClearText;

            if (el.ClearTextColor != Color.Default)
                view.ClearLabel.SetTextColor(el.ClearTextColor.ToAndroid());

            if (!String.IsNullOrWhiteSpace(el.PromptText))
                view.SignaturePrompt.Text = el.PromptText;

            if (el.PromptTextColor != Color.Default)
                view.SignaturePrompt.SetTextColor(el.PromptTextColor.ToAndroid());

            if (el.SignatureLineColor != Color.Default)
                view.SignatureLineColor = el.SignatureLineColor.ToAndroid();
                
            if (el.BackgroundColor != Color.Default)
                view.BackgroundColor = el.BackgroundColor.ToAndroid();

            if (el.StrokeColor != Color.Default)
                view.StrokeColor = el.StrokeColor.ToAndroid();

            if (el.StrokeWidth > 0)
                view.StrokeWidth = el.StrokeWidth;
            
            this.Element.SetInternals(
                this.GetImageStream,
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
                this.Control.Caption.SetTextColor(el.CaptionTextColor.ToAndroid());

            else if (e.PropertyName == SignaturePadView.ClearTextProperty.PropertyName)
                this.Control.ClearLabel.Text = el.ClearText;

            else if (e.PropertyName == SignaturePadView.ClearTextColorProperty.PropertyName)
                this.Control.ClearLabel.SetTextColor(el.ClearTextColor.ToAndroid());

            else if (e.PropertyName == SignaturePadView.PromptTextProperty.PropertyName)
                this.Control.SignaturePrompt.Text = el.PromptText;

            else if (e.PropertyName == SignaturePadView.PromptTextColorProperty.PropertyName)
                this.Control.SignaturePrompt.SetTextColor(el.PromptTextColor.ToAndroid());

            else if (e.PropertyName == SignaturePadView.SignatureLineColorProperty.PropertyName)
                this.Control.SignatureLineColor = el.SignatureLineColor.ToAndroid();

            else if (e.PropertyName == SignaturePadView.StrokeColorProperty.PropertyName)
                this.Control.StrokeColor = el.StrokeColor.ToAndroid();

            else if (e.PropertyName == SignaturePadView.StrokeWidthProperty.PropertyName)
                this.Control.StrokeWidth = el.StrokeWidth;
        }


        private Stream GetImageStream(ImageFormatType imgFormat) {
            using (var image = this.Control.GetImage()) { 
                var stream = new MemoryStream();
                var format = imgFormat == ImageFormatType.Png
                    ? Android.Graphics.Bitmap.CompressFormat.Png
                    : Android.Graphics.Bitmap.CompressFormat.Jpeg;
                image.Compress(format, 100, stream); // TODO: quality control
                return stream; // TODO: careful
            }
        }
    }
}
