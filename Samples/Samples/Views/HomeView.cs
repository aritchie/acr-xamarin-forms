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
                        this.Nav<SignatureListView, SignatureListViewModel>("SignaturePad Service"),
                        this.Nav<SignaturePadConfigView, SignaturePadConfigViewModel>("SignaturePad Service Configuration"),
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