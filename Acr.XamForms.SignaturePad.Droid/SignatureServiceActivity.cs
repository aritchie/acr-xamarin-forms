using System;
using System.IO;
using System.Linq;
using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Android.Widget.Button;
using NativeView = global::SignaturePad.SignaturePadView;
using RelativeLayout = Android.Widget.RelativeLayout;


namespace Acr.XamForms.SignaturePad.Droid {

    [Activity]
    public class SignaturePadActivity : Activity {
        private static readonly string fileStore;
        private NativeView signatureView;
        private Button btnSave;
        private Button btnCancel;


        static SignaturePadActivity() {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            fileStore = Path.Combine(path, "signature.tmp");
        }


        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.SignaturePad);

            var rootView = this.FindViewById<RelativeLayout>(Resource.Id.rootView);
            this.signatureView = this.FindViewById<NativeView>(Resource.Id.signatureView);
            this.btnSave = this.FindViewById<Button>(Resource.Id.btnSave);
            this.btnCancel = this.FindViewById<Button>(Resource.Id.btnCancel);

            var cfg = this.Resolve().CurrentConfig;
            rootView.SetBackgroundColor(cfg.MainBackgroundColor.ToAndroid());
            this.signatureView.BackgroundColor = cfg.SignatureBackgroundColor.ToAndroid();
            this.signatureView.Caption.Text = cfg.CaptionText;
            this.signatureView.Caption.SetTextColor(cfg.CaptionTextColor.ToAndroid());
            this.signatureView.ClearLabel.Text = cfg.ClearText;
            this.signatureView.ClearLabel.SetTextColor(cfg.ClearTextColor.ToAndroid());
            this.signatureView.SignatureLineColor = cfg.SignatureLineColor.ToAndroid(); 
            this.signatureView.SignaturePrompt.Text = cfg.PromptText;
            this.signatureView.SignaturePrompt.SetTextColor(cfg.PromptTextColor.ToAndroid());
            this.signatureView.StrokeColor = cfg.StrokeColor.ToAndroid();
            this.signatureView.StrokeWidth = cfg.StrokeWidth;

            this.btnSave.Text = cfg.SaveText;
            this.btnCancel.Text = cfg.CancelText;
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


        private SignatureService Resolve() {
            return DependencyService.Get<ISignatureService>() as SignatureService;
        }


        private void OnSave(object sender, EventArgs args) {
            if (this.signatureView.IsBlank)
                return;

            var points = this.signatureView
                .Points
                .Select(x => new DrawPoint(x.X, x.Y));

            var service = this.Resolve();

            using (var image = this.signatureView.GetImage()) {
                using (var fs = new FileStream(fileStore, FileMode.Create)) {
                    var format = service.CurrentConfig.ImageType == ImageFormatType.Png
                        ? Android.Graphics.Bitmap.CompressFormat.Png
                        : Android.Graphics.Bitmap.CompressFormat.Jpeg;
                    image.Compress(format, 100, fs);
                }
            }

            this.Finish();
            service.Complete(new SignatureResult(
                false, 
                () => new FileStream(fileStore, FileMode.Open, FileAccess.Read, FileShare.Read), 
                points
            ));
        }


        private void OnCancel(object sender, EventArgs args) {
            this.Resolve().Cancel();
            this.Finish();
        }
    }
}

