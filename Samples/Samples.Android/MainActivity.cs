using System;
using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace Samples.Droid {

    [Activity(Label = "ACR Xamarim Forms Examples", MainLauncher = true)]
	public class MainActivity : FormsApplicationActivity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
			this.LoadApplication(new Samples.App());
        }
    }
}

