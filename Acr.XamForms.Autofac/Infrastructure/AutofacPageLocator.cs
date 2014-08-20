using System;
using Acr.XamForms.Infrastructure;
using Acr.XamForms.ViewModels;
using Autofac;
using Xamarin.Forms;


namespace Acr.XamForms.Autofac.Infrastructure {
    
    public class AutofacPageLocator : PageLocator {
        private readonly ILifetimeScope container;


        public AutofacPageLocator(ILifetimeScope container) {
            this.container = container;
        }


        protected override Page CreatePage(Type pageType) {
            return this.container.Resolve(pageType) as Page;
        }


        protected override IViewModel CreateViewModel(Type viewModelType) {
            return this.container.Resolve(viewModelType) as IViewModel;
        }
    }
}
