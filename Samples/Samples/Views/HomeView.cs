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
<<<<<<< HEAD
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

=======
                        this.Nav<BarCodeView, BarCodeViewModel>("BarCode Scanning"),
                        this.Nav<UserDialogView, UserDialogViewModel>("User Dialogs"),
                        this.Nav<DeviceInfoView, DeviceInfoViewModel>("Device Information"),
						this.Nav<FileSystemView, FileSystemViewModel>("File System"),
                        this.Nav<NetworkView, NetworkViewModel>("Network"),
                        this.Nav<SettingsView, SettingsViewModel>("Settings"),
                        this.Nav<PhoneView, PhoneViewModel>("Phone Call"),
                        this.Nav<SmsView, SmsViewModel>("Send SMS"),
                        this.Nav<ConverterView, ConverterViewModel>("Converters"),
                        this.Nav<LocationView, LocationViewModel>("Location"),
                        this.Nav<PhotoView, PhotoViewModel>("Camera/Photo Gallery"),
                        this.Nav<TextToSpeechView, TextToSpeechViewModel>("Text-to-Speech"),
                        this.Nav<SignatureListView, SignatureListViewModel>("SignaturePad Service"),
                        this.Nav<SignaturePadConfigView, SignaturePadConfigViewModel>("SignaturePad Service Configuration"),
>>>>>>> dev
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


        private Button Nav<TPage, TViewModel>(string text)
                where TPage : ContentPage, new()
                where TViewModel : IViewModel {

            return new Button {
                Text = text,

                HorizontalOptions = LayoutOptions.FillAndExpand,
                Command = new Command(() => {
                    var vm = App.Resolve<TViewModel>();
                    var page = new TPage {
                        BindingContext = vm
                    };
                    this.Navigation.PushAsync(page);
                }),
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };
        }
    }
}