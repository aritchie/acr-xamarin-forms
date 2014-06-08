using System;
using Xamarin.Forms;


namespace Acr.XamForms.SignaturePad {

    public enum ImageFormatType {
        Png,
        Jpg
    }


    public class SignaturePadConfiguration {

        public ImageFormatType ImageType { get; set; }

        public string SaveText { get; set; }
        public string CancelText { get; set; }

        public Color? BackgroundColor { get; set; }
        public Color? SignatureLineColor { get; set; }
        
        public string CaptionText { get; set; }
        public Color? CaptionTextColor { get; set; }

        public string PromptText { get; set; }
        public Color? PromptTextColor { get; set; }

        public string ClearText { get; set; }
        public Color? ClearTextColor { get; set; }

        public float? StrokeWidth { get; set; }
        public Color? StrokeColor { get; set; }
    }
}
