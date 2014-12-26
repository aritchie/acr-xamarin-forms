using System;
using Acr.XamForms.BarCodeScanner;
using Acr.XamForms.Mobile.WindowsPhone;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.UserDialogs.WindowsPhone;
using Microsoft.Phone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;


namespace Samples.WinPhone {

    public partial class MainPage : FormsApplicationPage
    {

        public MainPage() {
            InitializeComponent();
            Forms.Init();
            new Logger();
            new UserDialogService();
            new SignaturePadConfiguration();
            new BarCodeResult(null, BarCodeFormat.AZTEC);

            LoadApplication(new Samples.App());
        }
    }
}
