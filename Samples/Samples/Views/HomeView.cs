using System;
using Acr.XamForms.ViewModels;
using Samples.ViewModels;
using Xamarin.Forms;


namespace Samples.Views {

    public class HomeView : ContentPage {

        public HomeView() {
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.Content = new ScrollView {
                //new Label {
                //    Text = "Home",
                //    Font = Font.BoldSystemFontOfSize(40),
                //    HorizontalOptions = LayoutOptions.Center
                //},
                Content = new StackLayout {
                    //HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Children = {
                        this.Nav<BarCodeViewModel>("BarCode Scanning"),
                        this.Nav<UserDialogViewModel>("User Dialogs"),
                        this.Nav<DeviceInfoViewModel>("Device Information"),
                        this.Nav<NetworkViewModel>("Network"),
                        this.Nav<SettingsViewModel>("Settings"),
                        this.Nav<PhoneViewModel>("Phone Call"),
                        this.Nav<SmsViewModel>("Send SMS"),
                        //this.Nav<MailViewModel>("Send E-Mail"),
                        this.Nav<LocationViewModel>("Location"),
                        this.Nav<PhotoViewModel>("Camera/Photo Gallery"),
                        this.Nav<TextToSpeechViewModel>("Text-to-Speech"),
                        this.Nav<SignatureListViewModel>("Signature Pad")
                    }
                }
            };
        }


        private Button Nav<T>(string text) where T : IViewModel {
            return new Button {
                Text = text,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Command = new Command(() => App.NavigateTo<T>()),
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };     
        }
    }
}