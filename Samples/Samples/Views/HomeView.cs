using System;
using Acr;
using Samples.ViewModels;
using Xamarin.Forms;
using Ninject;

namespace Samples.Views {

    public class HomeView : ContentPage {

        public HomeView() {
            this.Title = "ACR Xamarin Forms";
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            this.Content = new ScrollView {
                Content = new StackLayout {
                    Children = {
                        this.Nav<SignatureListView, SignatureListViewModel>("SignaturePad Service"),
                        this.Nav<SignaturePadConfigView, SignaturePadConfigViewModel>("SignaturePad Service Configuration"),
                        new Button {
                            Text = "XAML Signature Pad",
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Command = new Xamarin.Forms.Command(async () => {
                                if (Device.OS == TargetPlatform.WinPhone)
                                    await this.DisplayAlert("Not Support", "Windows Phone does not currently support the signature pad", "");
                                else
                                    await App.Navigation.PushAsync(new SignatureXamlView());
                            }),
                            Font = Font.SystemFontOfSize(NamedSize.Large)
                        }
                    }
                }
            };
        }


        private Button Nav<TPage, TViewModel>(string text)
                where TPage : ContentPage, new()
                where TViewModel : ViewModel {

            return new Button {
                Text = text,

                HorizontalOptions = LayoutOptions.FillAndExpand,
                Command = new Xamarin.Forms.Command(() => {
                    var vm = App.Kernel.Get<TViewModel>();
                    var page = new TPage {
                        BindingContext = vm
                    };
                    App.Navigation.PushAsync(page);
                }),
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };
        }
    }
}