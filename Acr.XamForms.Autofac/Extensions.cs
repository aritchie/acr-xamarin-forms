using System;
using System.Linq;
using System.Reflection;
using Acr.XamForms.Autofac.Infrastructure;
using Acr.XamForms.Infrastructure;
using Acr.XamForms.ViewModels;
using Autofac;
using Xamarin.Forms;


namespace Acr.XamForms.Autofac {
    
    public static class Extensions {


        public static ContainerBuilder RegisterMvvmComponents(this ContainerBuilder builder, params Assembly[] assemblies) {
            builder
                .RegisterType<AutofacPageLocator>()
                .As<IPageLocator>()
                .SingleInstance(); 
          
            builder
                .RegisterViewModels(assemblies)
                .RegisterViews(assemblies);

            return builder;
        }


        public static ContainerBuilder RegisterViewModels(this ContainerBuilder builder, params Assembly[] assemblies) {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x => 
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.GetTypeInfo().ImplementedInterfaces.Any(y => y == typeof(IViewModel))
                )
                .InstancePerDependency();

            return builder;
        }


       public static ContainerBuilder RegisterViews(this ContainerBuilder builder, params Assembly[] assemblies) {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x => 
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.GetTypeInfo().IsSubclassOf(typeof(Page))
                )
                .InstancePerDependency();

            return builder;
        }


        public static ContainerBuilder RegisterXamDependency<T>(this ContainerBuilder builder) where T : class {
            builder.Register(x => DependencyService.Get<T>()).SingleInstance();
            return builder;
        }
    }
}
