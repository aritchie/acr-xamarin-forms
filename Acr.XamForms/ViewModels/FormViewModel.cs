using System;
using System.Linq;
using System.Reflection;


namespace Acr.XamForms.ViewModels {
    
    public abstract class FormViewModel : ViewModel {

        protected FormViewModel() {
            this.GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .Where(x =>
                    x.CanWrite &&
                    x
                        .GetType()
                        .GetTypeInfo()
                        .ImplementedInterfaces
                        .Any(y => y == typeof(IProperty<>))
                )
                .ToList()
                .ForEach(x => {
                    var propertyInstance = Activator.CreateInstance(x.PropertyType);
                    x.SetValue(this, propertyInstance);
                });
        }
    }
}
