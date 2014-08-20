using System;
using System.Linq;
using System.Reflection;


namespace Acr.XamForms.ViewModels {
    
    public abstract class FormViewModel : ViewModel {

        private bool isValid;
        public bool IsValid {
            get { return this.isValid; }
            set { this.SetProperty(ref this.isValid, value); }
        }


        protected virtual void Validate() {
            this.IsValid = this
                .GetType()
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
                .All(x => x);
        }
    }
}
