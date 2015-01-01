using System;
using System.IO;
using System.Linq;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms.Platform.iOS;


namespace Acr.XamForms.SignaturePad.iOS {

    public class SignatureServiceController : UIViewController {

        private SignatureServiceView view;

        private IEnumerable<DrawPoint> points;
        private Action<SignatureResult> onResult;
        private readonly SignaturePadConfiguration config;


        public SignatureServiceController(SignaturePadConfiguration config, Action<SignatureResult> onResult) {
            this.config = config;
            this.onResult = onResult;
        }


        public SignatureServiceController(SignaturePadConfiguration config, IEnumerable<DrawPoint> points) {
            this.config = config;
            this.points = points;
        }


        public override void LoadView() {
            base.LoadView();

            this.view = new SignatureServiceView();
            this.View = this.view;
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            this.view.BackgroundColor = this.config.MainBackgroundColor.ToUIColor();
            this.view.Signature.BackgroundColor = this.config.SignatureBackgroundColor.ToUIColor();
            this.view.Signature.Caption.TextColor = this.config.CaptionTextColor.ToUIColor();
            this.view.Signature.Caption.Text = this.config.CaptionText;
            this.view.Signature.ClearLabel.SetTitle(this.config.ClearText, UIControlState.Normal);
            this.view.Signature.ClearLabel.SetTitleColor(this.config.ClearTextColor.ToUIColor(), UIControlState.Normal);
            this.view.Signature.SignatureLineColor = this.config.SignatureLineColor.ToUIColor();
            this.view.Signature.SignaturePrompt.Text = this.config.PromptText;
            this.view.Signature.SignaturePrompt.TextColor = this.config.PromptTextColor.ToUIColor();
            this.view.Signature.StrokeColor = this.config.StrokeColor.ToUIColor();
            this.view.Signature.StrokeWidth = this.config.StrokeWidth;
            this.view.Signature.Layer.ShadowOffset = new CGSize(0, 0);
            this.view.Signature.Layer.ShadowOpacity = 1f;

            if (this.onResult == null) {
                this.view.CancelButton.Hidden = true;
                this.view.SaveButton.Hidden = true;
                this.view.Signature.ClearLabel.Hidden = true;
                this.view.Signature.LoadPoints(this.points.Select(x => new CGPoint { X = x.X, Y = x.Y }).ToArray());
            }
            else {
                this.view.SaveButton.SetTitle(this.config.SaveText, UIControlState.Normal);
                this.view.SaveButton.TouchUpInside += (sender, args) => {
                    if (this.view.Signature.IsBlank)
                        return;

                    var points = this.view
                        .Signature
                        .Points
                        .Select(x => new DrawPoint(x.X, x.Y));

                    using (var image = this.view.Signature.GetImage()) 
                        using (var stream = GetImageStream(image, this.config.ImageType))
						    using (var fs = new FileStream("Signature.tmp", FileMode.Create)) 
							    stream.CopyTo(fs);

				    this.DismissViewController(true, (Action)null);
				    this.onResult(new SignatureResult(false, () => new FileStream("Signature.tmp", FileMode.Open, FileAccess.Read, FileShare.Read), points));
                };

                this.view.CancelButton.SetTitle(this.config.CancelText, UIControlState.Normal);
                this.view.CancelButton.TouchUpInside += (sender, args) => {
                    this.DismissViewController(true, (Action)null);
                    this.onResult(new SignatureResult(true, null, null));
                };
            }
        }


        public void LoadSignature(params CGPoint[] points) {
            this.view.Signature.LoadPoints(points);
        }


//        public override void TouchesBegan(NSSet touches, UIEvent evt) {
//            base.TouchesBegan(touches, evt);
//            if (this.onResult == null)
//                this.DismissViewController(true, null);
//        }


        private static Stream GetImageStream(UIImage image, ImageFormatType formatType) {
            if (formatType == ImageFormatType.Jpg)
                return image.AsJPEG().AsStream();

            return image.AsPNG().AsStream();
        }
    }
}


//            FROM XAMARIN SAMPLES
//            this.view.Signature.Caption.Font = UIFont.FromName ("Marker Felt", 16f);
//            this.view.Signature.SignaturePrompt.Font = UIFont.FromName ("Helvetica", 32f);
//            this.view.Signature.BackgroundColor = UIColor.FromRGB (255, 255, 200); // a light yellow.
//            this.view.Signature.BackgroundImageView.Image = UIImage.FromBundle ("logo-galaxy-black-64.png");
//            this.view.Signature.BackgroundImageView.Alpha = 0.0625f;
//            this.view.Signature.BackgroundImageView.ContentMode = UIViewContentMode.ScaleToFill;
//            this.view.Signature.BackgroundImageView.Frame = new System.Drawing.RectangleF(20, 20, 256, 256);