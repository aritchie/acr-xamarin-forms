using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using Windows.UI.Xaml.Media;
using System.ComponentModel;
using NativeView = Xamarin.Controls.SignaturePad;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.SignaturePad.Windows8;
using System.Linq;

[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]

namespace Acr.XamForms.SignaturePad.Windows8 {
    
    public class SignaturePadRenderer : ViewRenderer<SignaturePadView, NativeView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SignaturePadView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            var view = new NativeView();
            var el = e.NewElement;

            if (el.BackgroundColor != Color.Default)
                view.BackgroundColor = PlatformConverters.FormsToWindowsColor(el.BackgroundColor);

            if (!string.IsNullOrEmpty(el.CaptionText))
                view.Caption.Text = el.CaptionText;

            if (el.CaptionTextColor != Color.Default)
                view.Caption.Foreground = PlatformConverters.BrushFromColor(el.CaptionTextColor);

            if (!string.IsNullOrEmpty(el.ClearText))
                view.ClearLabel.Text = el.ClearText;

            if (el.ClearTextColor != Color.Default)
                view.ClearLabel.Foreground = PlatformConverters.BrushFromColor(el.ClearTextColor);

            if (!String.IsNullOrWhiteSpace(el.PromptText))
                view.SignaturePrompt.Text = el.PromptText;

            if (el.PromptTextColor != Color.Default)
                view.SignaturePrompt.Foreground = PlatformConverters.BrushFromColor(el.PromptTextColor);

            if (el.SignatureLineColor != Color.Default)
                view.SignatureLineColor = PlatformConverters.FormsToWindowsColor(el.SignatureLineColor);

            if (el.StrokeColor != Color.Default)
                view.Stroke =  new SolidColorBrush(PlatformConverters.FormsToWindowsColor(el.StrokeColor));

            if (el.StrokeWidth > 0)
                view.StrokeWidth = (int)el.StrokeWidth;

            this.Element.SetInternals(
                view.GetImage,
                () => view.Points.Select(x => new DrawPoint((float)x.X, (float)x.Y)),
                x => view.LoadPoints(x.Select(y => new Windows.Foundation.Point(System.Convert.ToDouble(y.X), System.Convert.ToDouble(y.Y))).ToArray()),
                () => view.IsBlank);

            this.SetNativeControl(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Element == null || this.Control == null)
                return;

            var el = this.Element;
            if (e.PropertyName == SignaturePadView.BackgroundColorProperty.PropertyName)
                this.Control.BackgroundColor = PlatformConverters.FormsToWindowsColor(el.BackgroundColor);

            else if (e.PropertyName == SignaturePadView.CaptionTextProperty.PropertyName)
                this.Control.Caption.Text = el.CaptionText;

            else if (e.PropertyName == SignaturePadView.CaptionTextColorProperty.PropertyName)
                this.Control.Caption.Foreground = PlatformConverters.BrushFromColor(el.CaptionTextColor);

            else if (e.PropertyName == SignaturePadView.ClearTextProperty.PropertyName)
                this.Control.ClearLabel.Text = el.ClearText;

            else if (e.PropertyName == SignaturePadView.ClearTextColorProperty.PropertyName)
                this.Control.ClearLabel.Foreground = PlatformConverters.BrushFromColor(el.ClearTextColor);

            else if (e.PropertyName == SignaturePadView.PromptTextProperty.PropertyName)
                this.Control.SignaturePrompt.Text = el.PromptText;

            else if (e.PropertyName == SignaturePadView.PromptTextColorProperty.PropertyName)
                this.Control.SignaturePrompt.Foreground = PlatformConverters.BrushFromColor(el.PromptTextColor);

            else if (e.PropertyName == SignaturePadView.SignatureLineColorProperty.PropertyName)
                this.Control.SignatureLineColor = PlatformConverters.FormsToWindowsColor(el.SignatureLineColor);

            else if (e.PropertyName == SignaturePadView.StrokeColorProperty.PropertyName)
                this.Control.Stroke = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(el.StrokeColor));

            else if (e.PropertyName == SignaturePadView.StrokeWidthProperty.PropertyName)
                this.Control.StrokeWidth = (int)el.StrokeWidth;
        }
    }
}