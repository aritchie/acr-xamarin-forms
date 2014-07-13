using System;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms.Infrastructure {
    
    public interface IViewModelResolver {

        ContentPage Resolve<TViewModel>(object args = null) where TViewModel : IViewModel;
    }
}
