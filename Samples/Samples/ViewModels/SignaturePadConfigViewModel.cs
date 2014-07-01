using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class ColorDefinition {
        public string Name { get; set; }
        public Color Color { get; set; }
    }


    public class SignaturePadConfigViewModel : ViewModel {
        private readonly ISignatureService signatureService;


        public SignaturePadConfigViewModel(ISignatureService signatureService) {
            this.signatureService = signatureService;
            this.Colors = typeof(Color)
                .GetTypeInfo()
                .DeclaredFields
                .Select(x => new ColorDefinition {
                    Name = x.Name,
                    Color = (Color)x.GetValue(null)
                })
                .ToList();

            var cfg = this.signatureService.Configuration;
            this.saveText = cfg.SaveText;
            this.cancelText = cfg.CancelText;
            this.promptText = cfg.PromptText;
            this.captionText = cfg.CaptionText;

            //this.bgColor = this.GetColorDefinition(cfg.BackgroundColor);
            //this.promptTextColor = this.GetColorDefinition(cfg.PromptTextColor);
            //this.captionTextColor = this.GetColorDefinition(cfg.CaptionTextColor);
            //this.signatureLineColor = this.GetColorDefinition(cfg.SignatureLineColor);
            //this.strokeColor = this.GetColorDefinition(cfg.StrokeColor);
        }


        #region Binding Properties

        public IList<ColorDefinition> Colors { get; private set; }


        private string cancelText;
        public string CancelText {
            get { return this.cancelText; }
            set { 
                if (this.SetProperty(ref this.cancelText, value))
                    this.signatureService.Configuration.CancelText = value;
            }
        }


        private string saveText;
        public string SaveText {
            get { return this.saveText; }
            set { 
                if (this.SetProperty(ref this.saveText, value))
                    this.signatureService.Configuration.SaveText = value;
            }
        }


        private string promptText;
        public string PromptText {
            get { return this.promptText; }
            set {
                if (this.SetProperty(ref this.promptText, value))
                    this.signatureService.Configuration.PromptText = value;
            }
        }


        private string captionText;
        public string CaptionText {
            get { return this.captionText; }
            set {
                if (this.SetProperty(ref this.captionText, value))
                    this.signatureService.Configuration.CaptionText = value;
            }
        }


        private float strokeWidth;
        public float StrokeWidth {
            get { return this.strokeWidth; }
            set {
                if (this.SetProperty(ref this.strokeWidth, value))
                    this.signatureService.Configuration.StrokeWidth = value;
            }
        }

        public ColorDefinition signatureLineColor;
        public ColorDefinition SignatureLineColor {
            get { return this.signatureLineColor; }
            set {
                if (this.SetProperty(ref this.signatureLineColor, value))
                    this.signatureService.Configuration.SignatureLineColor = value.Color;
            }
        }


        private ColorDefinition strokeColor;
        public ColorDefinition StrokeColor {
            get { return this.strokeColor; }
            set {
                if (this.SetProperty(ref this.strokeColor, value))
                    this.signatureService.Configuration.StrokeColor = value.Color;
            }
        }


        private ColorDefinition captionTextColor;
        public ColorDefinition CaptionTextColor {
            get { return this.captionTextColor; }
            set {
                if (this.SetProperty(ref this.captionTextColor, value))
                    this.signatureService.Configuration.CaptionTextColor = value.Color;
            }
        }


        private ColorDefinition bgColor;
        public ColorDefinition BgColor {
            get { return this.bgColor; }
            set {
                if (this.SetProperty(ref this.bgColor, value))
                    this.signatureService.Configuration.MainBackgroundColor = value.Color;
            }
        }


        private ColorDefinition promptTextColor;
        public ColorDefinition PromptTextColor {
            get { return this.promptTextColor; }
            set {
                if (this.SetProperty(ref this.promptTextColor, value))
                    this.signatureService.Configuration.PromptTextColor = value.Color;
            }
        }

        #endregion
    }
}
