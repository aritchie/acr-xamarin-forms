using System;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms.Infrastructure {
    
    public interface IViewModelResolver {

        IViewModel ResolveViewModel(Type viewModelType, object args = null);
        ContentPage ResolvePage(Type viewModelType, object args = null);
    }
}
