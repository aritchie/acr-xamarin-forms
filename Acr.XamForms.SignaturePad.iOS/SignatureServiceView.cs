using System;
using System.Drawing;
using MonoTouch.UIKit;
using NativeView = global::SignaturePad.SignaturePadView;


namespace Acr.XamForms.SignaturePad.iOS {

    public class SignatureServiceView : UIView {

        public NativeView Signature { get; set; }
        public UIButton SaveButton { get; private set; }
        public UIButton CancelButton { get; private set; }


        public SignatureServiceView() {
            //BackgroundColor = UIColor.White;
            this.SaveButton = UIButton.FromType(UIButtonType.RoundedRect);
            this.CancelButton = UIButton.FromType(UIButtonType.RoundedRect);
            this.Frame = UIScreen.MainScreen.ApplicationFrame;

            this.Signature = new NativeView();
            this.TranslatesAutoresizingMaskIntoConstraints = false;

            this.AddSubview(this.Signature);
            this.AddSubview(this.SaveButton);
            this.AddSubview(this.CancelButton);
        }


        public override void LayoutSubviews() {
            var frame = this.Frame;
            var sbframe = UIApplication.SharedApplication.StatusBarFrame;
            var portrait = UIApplication.SharedApplication.StatusBarOrientation.HasFlag(UIDeviceOrientation.Portrait);

            var width = portrait
                ? frame.Size.Width
                : frame.Size.Width - sbframe.Width;

            var height = portrait
                ? frame.Size.Height - sbframe.Height 
                : frame.Size.Height;

            var x = portrait
                ? 0
                : frame.Location.X + sbframe.Width;

            var y = portrait
                ? frame.Location.Y + sbframe.Height
                : 0;

            this.Frame = new RectangleF(x, y, width, height);

            this.Signature.Frame = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone
                ? new RectangleF (10, 10, Bounds.Width - 20, Bounds.Height - 60)
                : new RectangleF (84, 84, Bounds.Width - 168, Bounds.Width / 2);

            this.SaveButton.Frame = new RectangleF(10, this.Bounds.Height - 40, 120, 37);
            this.CancelButton.Frame = new RectangleF(this.Bounds.Width - 130, this.Bounds.Height - 40, 120, 37);
        }
    }
}