using System;
using Acr.XamForms.ViewModels;
using Samples.ViewModels;
using Xamarin.Forms;


namespace Samples.Views {

    public class HomeView : ContentPage {

        public HomeView() {
            this.Title = "ACR Xamarin Forms";
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.Content = new ScrollView {
                Content = new StackLayout {
                    Children = {
                        this.Nav<BarCodeViewModel>("BarCode Scanning"),
                        this.Nav<UserDialogViewModel>("User Dialogs"),
                        this.Nav<DeviceInfoViewModel>("Device Information"),
                        this.Nav<NetworkViewModel>("Network"),
                        this.Nav<SettingsViewModel>("Settings"),
                        this.Nav<PhoneViewModel>("Phone Call"),
                        this.Nav<SmsViewModel>("Send SMS"),
                        this.Nav<ConverterViewModel>("Converters"),
                        this.Nav<LocationViewModel>("Location"),
                        this.Nav<PhotoViewModel>("Camera/Photo Gallery"),
                        this.Nav<TextToSpeechViewModel>("Text-to-Speech"),
                        new Button {
                            Text = "Signature Pad Service",
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Command = new Command(async () => {
                                if (Device.OS == TargetPlatform.WinPhone)
                                    await this.DisplayAlert("Not Support", "Windows Phone does not currently support the signature pad", "");
                                else
                                    App.NavigateTo<SignatureListViewModel>();
                            }),
                            Font = Font.SystemFontOfSize(NamedSize.Large)
                        },

                        new Button {
                            Text = "XAML Signature Pad",
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Command = new Command(async () => {
                                if (Device.OS == TargetPlatform.WinPhone)
                                    await this.DisplayAlert("Not Support", "Windows Phone does not currently support the signature pad", "");
                                else
                                    await this.Navigation.PushAsync(new SignatureXamlView());
                            }),
                            Font = Font.SystemFontOfSize(NamedSize.Large)
                        }
                    }
                }
            };
        }


        private Button Nav<T>(string text) where T : IViewModel {
            return new Button {
                Text = text,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Command = new Command(() => App.NavigateTo<T>()),
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };     
        }
    }
}