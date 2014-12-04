using System;
using Acr.XamForms.BarCodeScanner;
using Acr.XamForms.Mobile.WindowsPhone;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.UserDialogs.WindowsPhone;
using Microsoft.Phone.Controls;
using Xamarin.Forms;


namespace Samples.WinPhone {

    public partial class MainPage : PhoneApplicationPage {

        public MainPage() {
            InitializeComponent();
            Forms.Init();
            new DeviceInfo();
            new UserDialogService();
            new SignaturePadConfiguration();
            new BarCodeResult(null, BarCodeFormat.AZTEC);

            this.Content = Samples.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
