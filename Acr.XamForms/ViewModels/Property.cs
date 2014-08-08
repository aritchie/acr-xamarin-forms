using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Acr.XamForms.ViewModels {
    
    public class Property<T> : AbstractNpcObject, IProperty {
        private readonly Action<T, IList<string>> onValidate;


        public Property(Action<T, IList<string>> onValidate) {
            this.Errors = new ObservableList<string>();
            this.onValidate = onValidate;
        } 


        public ObservableList<string> Errors { get; private set; }
        //public bool RejectBadInput { get; set; }

        private T value;
        public virtual T Value {
            get { return this.value; }
            set { this.SetProperty(ref this.value, value); }
        }


        public bool isValid;
        public bool IsValid {
            get { return this.isValid; }
            private set { this.SetProperty(ref this.isValid, value); }
        }


        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            this.RunValidation();
        }


        protected void RunValidation() {
            this.Errors.Clear();
            var errors = this.Validate();

            if (errors == null || !errors.Any()) 
                this.isValid = true;
            else {
                this.Errors.AddRange(errors);
                this.isValid = false;
            }
        }

        public virtual IEnumerable<string> Validate() {
            var list = new List<string>();
            this.onValidate(this.Value, list);
            return list;
        }
    }
}
