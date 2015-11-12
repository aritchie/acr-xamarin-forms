using System;
using Samples.Views;
using Xamarin.Forms;
using Acr;
using Ninject;

namespace Samples {

    public class App : Xamarin.Forms.Application {

        public static IKernel Kernel { get; private set; }
        public static INavigation Navigation { get; private set; }

        public App() {
            Kernel = new StandardKernel(new SamplesNinjectModule());
            this.MainPage = new NavigationPage(new HomeView());
            Navigation = this.MainPage.Navigation;
		}
    }
}
