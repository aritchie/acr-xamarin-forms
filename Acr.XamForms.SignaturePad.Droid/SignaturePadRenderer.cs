using System;
using Acr.XamForms.SignaturePad.Droid;
using Acr.XamForms.SignaturePad.Views;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SignaturePadView), typeof(SignaturePadRenderer))]


namespace Acr.XamForms.SignaturePad.Droid {
    
    public class SignaturePadRenderer : IViewRenderer {

        #region IViewRenderer Members

        public Xamarin.Forms.Size MinimumSize() {
            throw new NotImplementedException();
        }

        public Xamarin.Forms.VisualElement Model {
            get { throw new NotImplementedException(); }
        }

        public void SetModel(Xamarin.Forms.VisualElement view) {
            throw new NotImplementedException();
        }

        public ViewTracker Tracker {
            get { throw new NotImplementedException(); }
        }

        public void UpdateLayout() {
            throw new NotImplementedException();
        }

        public ViewGroup ViewGroup {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
/*
    [Activity]
    public class SignaturePadActivity : Activity {
        private SignaturePadView signatureView;
        private Button btnSave;
        private Button btnCancel;


        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.SignaturePad);

            this.signatureView = this.FindViewById<SignaturePadView>(Resource.Id.signatureView);
            this.btnSave = this.FindViewById<Button>(Resource.Id.btnSave);
            this.btnCancel = this.FindViewById<Button>(Resource.Id.btnCancel);

            var cfg = DroidSignatureService.CurrentConfig;
            this.signatureView.BackgroundColor = cfg.BackgroundColor.ToAndroidColor();
            this.signatureView.Caption.Text = cfg.CaptionText;
            this.signatureView.Caption.SetTextColor(cfg.CaptionTextColor.ToAndroidColor());
            this.signatureView.ClearLabel.Text = cfg.ClearText;
            this.signatureView.ClearLabel.SetTextColor(cfg.ClearTextColor.ToAndroidColor());
            this.signatureView.SignatureLineColor = cfg.SignatureLineColor.ToAndroidColor(); 
            this.signatureView.SignaturePrompt.Text = cfg.PromptText;
            this.signatureView.SignaturePrompt.SetTextColor(cfg.PromptColor.ToAndroidColor());
            this.signatureView.StrokeColor = cfg.StrokeColor.ToAndroidColor();
            this.signatureView.StrokeWidth = cfg.StrokeWidth.Value;

            this.btnSave.Text = cfg.SaveText;
            this.btnCancel.Text = cfg.CancelText;

            if (DroidSignatureService.CurrentPoints != null) {
                this.btnSave.Visibility = ViewStates.Gone;
                this.btnCancel.Visibility = ViewStates.Gone;
//                this.signatureView.Enabled = false;
                this.signatureView.LoadPoints(
                    DroidSignatureService
                        .CurrentPoints
                        .Select(x => new PointF { X = x.X, Y = x.Y })
                        .ToArray()
                );
            }
        }


        protected override void OnResume() {
            base.OnResume();
            this.btnSave.Click += this.OnSave;
            this.btnCancel.Click += this.OnCancel;
        }


        protected override void OnPause() {
            base.OnPause();
            this.btnSave.Click -= this.OnSave;
            this.btnCancel.Click -= this.OnCancel;
        }


        private void OnSave(object sender, EventArgs args) {
            if (this.signatureView.IsBlank)
                return;

            var points = this.signatureView
                .Points
                .Select(x => new DrawPoint(x.X, x.Y));

            using (var image = this.signatureView.GetImage()) {
                 using (var stream = new MemoryStream()) {
                    var format = DroidSignatureService.CurrentConfig.ImageType == ImageFormatType.Png
                        ? Android.Graphics.Bitmap.CompressFormat.Png
                        : Android.Graphics.Bitmap.CompressFormat.Jpeg;
                    image.Compress(format, 100, stream);
                    DroidSignatureService.OnResult(new SignatureResult(false, stream, points));
                    this.Finish();
                }
            }
        }


        private void OnCancel(object sender, EventArgs args) {
            DroidSignatureService.OnResult(new SignatureResult(true, null, null));
            this.Finish();
        }
    }
 * 
<RelativeLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="fill_parent"
    android:layout_height="wrap_content"
    android:minWidth="25px"
    android:minHeight="25px"
    android:background="#ff777777"
    android:id="@+id/rootView">
    <signaturepad.SignaturePadView
        android:id="@+id/signatureView"
        android:layout_width="fill_parent"
        android:layout_height="fill_parent"
        android:layout_marginLeft="10dp"
        android:layout_marginTop="10dp"
        android:layout_marginRight="10dp"
        android:layout_marginBottom="70dp" />
    <Button
        android:id="@+id/btnCancel"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:text="Cancel"
        android:layout_marginLeft="10dp"
        android:layout_marginRight="10dp"
        android:layout_alignParentBottom="true"
        android:layout_alignParentLeft="true"
        android:layout_marginBottom="10dp" />
    <Button
        android:id="@+id/btnSave"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:text="Save"
        android:layout_marginLeft="10dp"
        android:layout_marginRight="10dp"
        android:layout_alignParentBottom="true"
        android:layout_alignParentRight="true"
        android:layout_marginBottom="10dp" />
</RelativeLayout>*/