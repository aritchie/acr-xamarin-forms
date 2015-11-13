using System;
using Xamarin.Forms;


namespace Acr.XamForms.SignaturePad {

    public enum ImageFormatType {
        Png,
        Jpg
    }


    public class SignaturePadConfiguration {
		private static SignaturePadConfiguration defaultConfig;
		public static SignaturePadConfiguration Default {
			get {
				defaultConfig = defaultConfig ?? new SignaturePadConfiguration();
				return defaultConfig;
			}
			set {
				if (defaultConfig == null)
					throw new ArgumentException("Default configuration cannot be null");

				defaultConfig = value;
			}
		}


		public SignaturePadConfiguration() {
			this.ImageType = ImageFormatType.Png;
			this.MainBackgroundColor = Color.White;
			this.CaptionTextColor = Color.Black;
			this.ClearTextColor = Color.Black;
			this.PromptTextColor = Color.White;
			this.StrokeColor = Color.Black;
			this.StrokeWidth = 2f;
			this.SignatureBackgroundColor = Color.White;
			this.SignatureLineColor = Color.Black;
            this.SaveTextColor = Color.Black;
            this.SaveButtonColor = Color.Gray;
            this.CanceTextColor = Color.Black;
            this.CanceButtonColor = Color.Gray;

            this.SaveText = "Save";
			this.CancelText = "Cancel";
			this.ClearText = "Clear";
			this.PromptText = "";
			this.CaptionText = "Please Sign Here";
		}


        public ImageFormatType ImageType { get; set; }

        public string SaveText { get; set; }
        public Color SaveTextColor { get; set; }
        public Color SaveButtonColor { get; set; }

        public string CancelText { get; set; }
        public Color CanceTextColor { get; set; }
        public Color CanceButtonColor { get; set; }

        public Color MainBackgroundColor { get; set; }
        public Color SignatureBackgroundColor { get; set; }
        public Color SignatureLineColor { get; set; }

        public string CaptionText { get; set; }
        public Color CaptionTextColor { get; set; }

        public string PromptText { get; set; }
        public Color PromptTextColor { get; set; }

        public string ClearText { get; set; }
        public Color ClearTextColor { get; set; }

        public float StrokeWidth { get; set; }
        public Color StrokeColor { get; set; }
    }
}
