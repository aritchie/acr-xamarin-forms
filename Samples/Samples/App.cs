using System;
using System.Reflection;
using Acr.XamForms.BarCodeScanner;
using Acr.XamForms.Mobile;
using Acr.XamForms.Mobile.IO;
using Acr.XamForms.Mobile.Locations;
using Acr.XamForms.Mobile.Media;
using Acr.XamForms.Mobile.Net;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.UserDialogs;
using Autofac;
using Samples.Views;
using Xamarin.Forms;


namespace Samples {

    public static class App {
        public static IContainer Container { get; private set; }


        public static T Resolve<T>() {
            return Container.Resolve<T>();
        }


        public static void Init() {
            if (Container != null)
                return;

            var builder = new ContainerBuilder();
            RegisterXamService<IBarCodeService>(builder);
            RegisterXamService<IDeviceInfo>(builder);
            RegisterXamService<IGeoLocator>(builder);
            RegisterXamService<IFileSystem>(builder);
            RegisterXamService<INetworkService>(builder);
            RegisterXamService<IPhoneService>(builder);
            RegisterXamService<IMediaPicker>(builder);
            RegisterXamService<ISettings>(builder);
            RegisterXamService<ITextToSpeechService>(builder);
            RegisterXamService<IUserDialogService>(builder);
            RegisterXamService<ISignatureService>(builder);

            builder
                .RegisterAssemblyTypes(typeof(App).GetTypeInfo().Assembly)
                .Where(x => x.Namespace.Equals("samples.viewmodels", StringComparison.CurrentCultureIgnoreCase))
                .AsSelf()
                .InstancePerDependency();

            Container = builder.Build();
        }


        private static void RegisterXamService<T>(ContainerBuilder builder) where T : class {
            builder
                .Register(x => DependencyService.Get<T>())
                .SingleInstance();
        }


        public static Page GetMainPage() {
            Init();
            var page = new NavigationPage(new HomeView());
            return page;
        }
    }
}
