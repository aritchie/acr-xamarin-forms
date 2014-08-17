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
            this.IsValid = true;
        } 


        public ObservableList<string> Errors { get; private set; }
        public bool HasBeenBound { get; private set; }
        public bool IsValid { get; private set; }
        public bool IsInvalid { get { return !this.IsValid; }} // useful for xaml
        public T OriginalValue { get; private set; }


        private T value;
        public T Value {
            get { return this.value; }
            set { this.SetProperty(ref this.value, value); }
        }


        public virtual bool IsDirty() {
            return !this.Value.Equals(this.OriginalValue); 
        }


        public virtual IEnumerable<string> Validate() {
            var list = new List<string>();
            this.onValidate(this.Value, list);
            return list;
        }


        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            if (propertyName != "Value")
                return;

            // TODO: should throttle validation
            if (this.HasBeenBound) 
                this.RunValidation();
            else {
                this.HasBeenBound = true;
                this.OriginalValue = value;
            }
        }


        protected void RunValidation() {
            this.Errors.Clear();

            var errors = this.Validate();
            if (errors != null && errors.Any()) 
                this.Errors.AddRange(errors);

            this.IsValid = !this.Errors.Any();
            this.OnPropertyChanged("IsValid");
            this.OnPropertyChanged("IsInvalid");
        }
    }
}
