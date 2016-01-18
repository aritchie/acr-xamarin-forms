//
// SampleView.cs: View to display a sample of the SignaturePadView.
//
// Author:
//   Timothy Risi (timothy.risi@gmail.com)
//
// Copyright (C) 2012 Timothy Risi
//
using System;
using System.Drawing;


#if __UNIFIED__
using UIKit;
using CoreGraphics;
using ObjCRuntime;
#else
using MonoTouch.UIKit;
using Constants = MonoTouch.Constants;
using CGPoint = global::System.Drawing.PointF;
using CGRect = global::System.Drawing.RectangleF;
#endif

using SignaturePad;

namespace Sample {
	public class SampleView : UIView {
		public SignaturePadView Signature { get; set; }
		UIImageView imageView;
		UIButton btnSave, btnLoad;
		CGPoint [] points;

		public SampleView ()
		{
			BackgroundColor = UIColor.White;

			//Create the save button
			btnSave = UIButton.FromType (UIButtonType.RoundedRect);
			btnSave.SetTitle ("Save", UIControlState.Normal);

			//Create the load button
			btnLoad = UIButton.FromType (UIButtonType.RoundedRect);
			btnLoad.SetTitle ("Load Last", UIControlState.Normal);
			btnLoad.TouchUpInside += (sender, e) => {
				if (points != null)
					Signature.LoadPoints (points);
			};

			Frame = UIScreen.MainScreen.ApplicationFrame;

			Signature = new SignaturePadView ();
			//Using different layouts for the iPhone and iPad, so setup device specific requirements here.
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {

				//iPhone version simply saves the vector of points in an instance variable.
				btnSave.TouchUpInside += (sender, e) => {
					if (Signature.IsBlank)
						new UIAlertView ("", "No signature to save.", null, "Okay", null).Show ();
					else {
						points = Signature.Points;
						new UIAlertView ("", "Vector Saved.", null, "Okay", null).Show ();
					}
				};
			} else {

				//iPad version saves the vector of points as well as retrieving the UIImage to display
				//in a UIImageView.
				btnSave.TouchUpInside += (sender, e) => {
					//if (signature.IsBlank)
					//	new UIAlertView ("", "No signature to save.", null, "Okay", null).Show ();
					imageView.Image = Signature.GetImage ();
					points = Signature.Points;
				};

				//Create the UIImageView to display a saved signature.
				imageView = new UIImageView();
				AddSubview(imageView);
			}
			TranslatesAutoresizingMaskIntoConstraints = false;

			//Add the subviews.
			AddSubview (Signature);
			AddSubview (btnSave);
			AddSubview (btnLoad);
		}

		public override void LayoutSubviews ()
		{
			if (new Version(Constants.Version) >= new Version (7, 0))
			{
				var frame = Frame;

				var width = UIApplication.SharedApplication.StatusBarOrientation.HasFlag(UIInterfaceOrientation.Portrait)
					? frame.Size.Width
						: frame.Size.Width - UIApplication.SharedApplication.StatusBarFrame.Width ;

                var height = UIApplication.SharedApplication.StatusBarOrientation.HasFlag (UIInterfaceOrientation.Portrait)
					? frame.Size.Height - UIApplication.SharedApplication.StatusBarFrame.Height 
						: frame.Size.Height;

                var x = UIApplication.SharedApplication.StatusBarOrientation.HasFlag (UIInterfaceOrientation.Portrait)
					? 0
						: frame.Location.X + UIApplication.SharedApplication.StatusBarFrame.Width;

                var y = UIApplication.SharedApplication.StatusBarOrientation.HasFlag (UIInterfaceOrientation.Portrait)
					? frame.Location.Y + UIApplication.SharedApplication.StatusBarFrame.Height
						: 0;

				Frame = new CGRect (x, y, width, height);
			}

			///Using different layouts for the iPhone and iPad, so setup device specific requirements here.
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
				Signature.Frame = new CGRect (10, 10, Bounds.Width - 20, Bounds.Height - 60);
			else {
				Signature.Frame = new CGRect (84, 84, Bounds.Width - 168, Bounds.Width / 2);
				imageView.Frame = new CGRect (84, Signature.Frame.Height + 168,
				                                   Frame.Width - 168, Frame.Width / 2);
			}

			//Button locations are based on the Frame, so must have their own frames set after the view's
			//Frame has been set.
			btnSave.Frame = new CGRect (10, Bounds.Height - 40, 120, 37);
			btnLoad.Frame = new CGRect (Bounds.Width - 130, Bounds.Height - 40, 120, 37);
		}
	}
}

