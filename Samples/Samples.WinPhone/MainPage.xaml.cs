using System;
using Microsoft.Phone.Controls;
using Xamarin.Forms;


namespace Samples.WinPhone {

    public partial class MainPage : PhoneApplicationPage {

        public MainPage() {
            InitializeComponent();
            Forms.Init();
            this.Content = Samples.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
