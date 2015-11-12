using System;
using Acr.XamForms.SignaturePad;
using Microsoft.Phone.Controls;
using Xamarin.Forms;


namespace Samples.WinPhone {

	public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage {

        public MainPage() {
            InitializeComponent();
            Forms.Init();
            new SignaturePadConfiguration();
			this.LoadApplication(new Samples.App());
        }
    }
}
