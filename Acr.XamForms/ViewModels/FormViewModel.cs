using System;
using System.Linq;
using System.Reflection;


namespace Acr.XamForms.ViewModels {
    
    public abstract class FormViewModel : ViewModel {

        protected virtual void Validate() {
            var propIsValid = this.GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .Where(x => x
                    .GetType()
                    .GetTypeInfo()
                    .ImplementedInterfaces
                    .Any(y => y == typeof(IProperty))
                )
                .Cast<IProperty>()
                .Select(x => {
                    x.Validate();
                    return x.IsValid;
                })
                .Any(x => x);

            // TODO: validate model now
        }
    }
}
