using System;
using CoreGraphics;
using UIKit;
using NativeView = SignaturePad.SignaturePadView;


namespace SignaturePad.iOS
{
	public class SignatureServiceView : UIView
	{
		public NativeView Signature { get; set; }
		public UIButton SaveButton { get; private set; }
		public UIButton CancelButton { get; private set; }


		public SignatureServiceView()
		{
			//BackgroundColor = UIColor.White;
			this.SaveButton = UIButton.FromType(UIButtonType.RoundedRect);
			this.CancelButton = UIButton.FromType(UIButtonType.RoundedRect);
			this.Frame = (CGRect)UIScreen.MainScreen.ApplicationFrame;

			this.Signature = new NativeView();
			this.TranslatesAutoresizingMaskIntoConstraints = false;

			this.AddSubview(this.Signature);
			this.AddSubview(this.SaveButton);
			this.AddSubview(this.CancelButton);
		}


		public override void LayoutSubviews()
		{
			var frame = UIScreen.MainScreen.ApplicationFrame;
			var sbframe = (CGRect)UIApplication.SharedApplication.StatusBarFrame;
			var portrait = UIApplication.SharedApplication.StatusBarOrientation.HasFlag(UIInterfaceOrientation.Portrait);


			this.Frame = new CGRect(0, frame.Location.Y, frame.Size.Width, frame.Size.Height);

			this.Signature.Frame = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone
				? new CGRect(10, 10, Frame.Width - 20, Frame.Height - 60)
				: new CGRect(84, 84, Frame.Width - 168, Frame.Width / 2);
			if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone && !portrait){
				this.Signature.Frame = new CGRect (10, 10, Frame.Width - 20, Frame.Height - 60);
			}

			this.SaveButton.Frame = new CGRect(10, this.Frame.Height - 40, 120, 37);
			this.CancelButton.Frame = new CGRect(this.Frame.Width - 130, this.Frame.Height - 40, 120, 37);
			this.SetNeedsDisplay ();
		}
	}
} 