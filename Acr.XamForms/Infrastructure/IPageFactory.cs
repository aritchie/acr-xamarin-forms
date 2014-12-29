using System;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Acr.XamForms.Infrastructure {

    public interface IPageFactory {

        Page CreateInstance(Type pageType, IViewModel viewModel);
    }
}
