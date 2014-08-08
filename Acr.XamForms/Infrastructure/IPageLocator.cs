using System;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms.Infrastructure {
    
    public interface IPageLocator {

        Page ResolvePageAndViewModel(Type viewModelType, object args = null);
        Page ResolvePage(IViewModel viewModel);
    }
}
