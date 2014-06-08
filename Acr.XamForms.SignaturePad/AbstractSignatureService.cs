using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace Acr.XamForms.SignaturePad {

    public abstract class AbstractSignatureService : ISignatureService {

        protected AbstractSignatureService() {
            this.DefaultConfiguration = new SignaturePadConfiguration {
                ImageType = ImageFormatType.Png,
                BackgroundColor = Color.White,
                CaptionTextColor = Color.Black,
                ClearTextColor = Color.Black,
                PromptTextColor = Color.White,
                StrokeColor = Color.Black,
                StrokeWidth = 2f,
                SignatureLineColor = Color.Black,

                SaveText = "Save",
                CancelText = "Cancel",
                ClearText = "Clear",
                PromptText = "",
                CaptionText = "Please Sign Here"
            };
        }


        protected abstract void GetSignature(Action<SignatureResult> onResult, SignaturePadConfiguration cfg);
        protected abstract void Load(IEnumerable<DrawPoint> points, SignaturePadConfiguration cfg);


        protected virtual void EnsureConfiguration(ref SignaturePadConfiguration cfg) {
            if (cfg == null)
                cfg = new SignaturePadConfiguration();

            if (cfg.BackgroundColor == null)
                cfg.BackgroundColor = this.DefaultConfiguration.BackgroundColor;

            if (cfg.CancelText == null)
                cfg.CancelText = this.DefaultConfiguration.CancelText;

            if (cfg.CaptionText == null)
                cfg.CaptionText = this.DefaultConfiguration.CaptionText;

            if (cfg.CaptionTextColor == null)
                cfg.CaptionTextColor = this.DefaultConfiguration.CaptionTextColor;
            
            if (cfg.ClearText == null)
                cfg.ClearText = this.DefaultConfiguration.ClearText;

            if (cfg.ClearTextColor == null)
                cfg.ClearTextColor = this.DefaultConfiguration.ClearTextColor;

            if (cfg.PromptTextColor == null)
                cfg.PromptTextColor = this.DefaultConfiguration.PromptTextColor;

            if (cfg.PromptText == null)
                cfg.PromptText = this.DefaultConfiguration.PromptText;

            if (cfg.SaveText == null)
                cfg.SaveText = this.DefaultConfiguration.SaveText;

            if (cfg.SignatureLineColor == null)
                cfg.SignatureLineColor = this.DefaultConfiguration.SignatureLineColor;

            if (cfg.StrokeColor == null)
                cfg.StrokeColor = this.DefaultConfiguration.StrokeColor;

            if (cfg.StrokeWidth == null)
                cfg.StrokeWidth = this.DefaultConfiguration.StrokeWidth;
        }


        public virtual void LoadSignature(IEnumerable<DrawPoint> points, SignaturePadConfiguration cfg) {
            this.EnsureConfiguration(ref cfg);
            this.Load(points, cfg);
        }


        //public virtual Task<SignatureResult> RequestSignatureAsync(SignaturePadConfiguration cfg) {
        //    var tcs = new TaskCompletionSource<SignatureResult>();
        //    this.RequestSignature(x => tcs.SetResult(x), cfg);
        //    return tcs.Task;
        //}


        public virtual void RequestSignature(Action<SignatureResult> onResult, SignaturePadConfiguration cfg) {
            this.EnsureConfiguration(ref cfg);
            this.GetSignature(onResult, cfg);
        }
        
        
        public SignaturePadConfiguration DefaultConfiguration { get; private set; }
    }
}

