using System;


namespace Acr.XamForms.Infrastructure {

    public interface IPageLocator {

        Type ResolvePageType(Type viewModelType);
    }
}
