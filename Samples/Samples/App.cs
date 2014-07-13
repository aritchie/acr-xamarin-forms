using System;
using System.Reflection;
using Acr.XamForms.BarCodeScanner;
using Acr.XamForms.Infrastructure;
using Acr.XamForms.Mobile;
using Acr.XamForms.Mobile.IO;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Autofac;
using Samples.Views;
using Xamarin.Forms;


namespace Samples {

    public static class App {
        private static IContainer container;
        private static INavigation navigator;
        private static bool init = false;


        public static void Init() {
            if (init)
                return;

            init = true;
            var builder = new ContainerBuilder()
                .RegisterViewModels()
                .RegisterXamDependency<IBarCodeScanner>()
                .RegisterXamDependency<IDeviceInfo>()
                .RegisterXamDependency<IFileViewer>()
                .RegisterXamDependency<ILocationService>()
                .RegisterXamDependency<ILogger>()
                .RegisterXamDependency<IFileSystem>()
                //.RegisterXamDependency<IMailService>()
                .RegisterXamDependency<INetworkService>()
                .RegisterXamDependency<IPhoneService>()
                .RegisterXamDependency<IPhotoService>()
                .RegisterXamDependency<ISettings>()
                .RegisterXamDependency<ITextToSpeechService>()
                .RegisterXamDependency<IUserDialogService>()
                .RegisterXamDependency<ISignatureService>();

            builder
                .Register(x => new ViewModelResolver(vt => container.Resolve(vt) as IViewModel))
                .As<IViewModelResolver>()
                .SingleInstance();

            container = builder.Build();
        }


        public static Page GetMainPage() {
            Init();
            var page = new NavigationPage(new HomeView());
            navigator = page.Navigation;
            return page;
        }


        private static ContainerBuilder RegisterViewModels(this ContainerBuilder builder) {
            var ass = Assembly.Load(new AssemblyName("Samples"));

            builder
                .RegisterAssemblyTypes(ass)
                .Where(x => 
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.Name.EndsWith("ViewModel")
                )
                .InstancePerDependency();

            return builder;
        }


        private static ContainerBuilder RegisterXamDependency<T>(this ContainerBuilder builder) where T : class {
            builder.Register(x => DependencyService.Get<T>()).SingleInstance();
            return builder;
        }


        public static void NavigateTo<T>(object args = null) where T : IViewModel {
            var page = container
                .Resolve<IViewModelResolver>()
                .ResolvePage(typeof(T), args);

            navigator.PushAsync(page);
        }


        public static T Resolve<T>() where T : class {
            return container.Resolve<T>();
        }
    }
}
