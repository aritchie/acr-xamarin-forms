using System;
using System.Collections.Generic;


namespace Acr.XamForms.Infrastructure {

    public interface IServiceLocator {

        void RegisterSingleton<TService, TImpl>();
        void RegisterType<TService, TImpl>();

        T Resolve<T>(string name);
        IEnumerable<T> ResolveAll<T>();
    }
}
