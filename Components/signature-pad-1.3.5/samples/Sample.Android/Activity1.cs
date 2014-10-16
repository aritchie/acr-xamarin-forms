using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using SignaturePad;

namespace Sample.Android {
	[Activity (Label = "Sample.Android", MainLauncher = true)]
	public class Activity1 : Activity {
		System.Drawing.PointF [] points;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//RequestedOrientation = global::Android.Content.PM.ScreenOrientation.Landscape;

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			SignaturePadView signature = FindViewById<SignaturePadView> (Resource.Id.signatureView);

			if (true) { // Customization activated
				View root = FindViewById<View> (Resource.Id.rootView);
				root.SetBackgroundColor (Color.White);

				// Activate this to internally use a bitmap to store the strokes
				// (good for frequent-redraw situations, bad for memory footprint)
				// signature.UseBitmapBuffer = true;

				signature.Caption.Text = "Authorization Signature";
				signature.Caption.SetTypeface (Typeface.Serif, TypefaceStyle.BoldItalic);
				signature.Caption.SetTextSize (global::Android.Util.ComplexUnitType.Sp, 16f);
				signature.SignaturePrompt.Text = ">>";
				signature.SignaturePrompt.SetTypeface (Typeface.SansSerif, TypefaceStyle.Normal);
				signature.SignaturePrompt.SetTextSize (global::Android.Util.ComplexUnitType.Sp, 32f);
				signature.BackgroundColor = Color.Rgb (255, 255, 200); // a light yellow.
				signature.StrokeColor = Color.Black;

				signature.BackgroundImageView.SetImageResource (Resource.Drawable.logo_galaxy_black_64);
				signature.BackgroundImageView.SetAlpha (16);
				signature.BackgroundImageView.SetAdjustViewBounds (true);
				var layout = new RelativeLayout.LayoutParams (RelativeLayout.LayoutParams.FillParent, RelativeLayout.LayoutParams.FillParent);
				layout.AddRule (LayoutRules.CenterInParent);
				layout.SetMargins (20, 20, 20, 20);
				signature.BackgroundImageView.LayoutParameters = layout;

				// You can change paddings for positioning...
				var caption = signature.Caption;
				caption.SetPadding (caption.PaddingLeft, 1, caption.PaddingRight, 25);
			}

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnSave = FindViewById<Button> (Resource.Id.btnSave);
			btnSave.Click += delegate {
				if (signature.IsBlank)
				{//Display the base line for the user to sign on.
					AlertDialog.Builder alert = new AlertDialog.Builder (this);
					alert.SetMessage ("No signature to save.");
					alert.SetNeutralButton ("Okay", delegate { });
					alert.Create ().Show ();
				}
				points = signature.Points;
			};
			btnSave.Dispose ();

			Button btnLoad = FindViewById<Button> (Resource.Id.btnLoad);
			btnLoad.Click += delegate {
				if (points != null)
					signature.LoadPoints (points);
			};
			btnLoad.Dispose ();
		}
	}
}


