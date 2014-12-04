using System;
using System.Reflection;
using Acr.XamForms.Autofac;
using Acr.XamForms.BarCodeScanner;
using Acr.XamForms.Infrastructure;
using Acr.XamForms.Mobile;
using Acr.XamForms.Mobile.IO;
using Acr.XamForms.Mobile.Locations;
using Acr.XamForms.Mobile.Media;
using Acr.XamForms.Mobile.Net;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Autofac;
using Samples.Views;
using Xamarin.Forms;


namespace Samples {

    public static class App {
        //private static IContainer container;
        public static IContainer Container { get; private set; }
        private static INavigation navigator;


        public static void Init() {
            if (Container != null)
                return;

            Container = new ContainerBuilder()
                .RegisterMvvmComponents(typeof(App).GetTypeInfo().Assembly)
                .RegisterXamDependency<IBarCodeService>()
                .RegisterXamDependency<IDeviceInfo>()
                .RegisterXamDependency<IGeoLocator>()
                .RegisterXamDependency<ILogger>()
                .RegisterXamDependency<IFileSystem>()
                //.RegisterXamDependency<IMailService>()
                .RegisterXamDependency<INetworkService>()
                .RegisterXamDependency<IPhoneService>()
                .RegisterXamDependency<IMediaPicker>()
                .RegisterXamDependency<ISettings>()
                .RegisterXamDependency<ITextToSpeechService>()
                .RegisterXamDependency<IUserDialogService>()
                .RegisterXamDependency<ISignatureService>()
                .Build();
        }


        public static Page GetMainPage() {
            Init();
            var page = new NavigationPage(new HomeView());
            navigator = page.Navigation;
            return page;
        }


        public static void NavigateTo<T>(object args = null) where T : IViewModel {
            var page = Container
                .Resolve<IPageLocator>()
                .ResolvePageAndViewModel(typeof(T), args);

            navigator.PushAsync(page);
        }
    }
}
