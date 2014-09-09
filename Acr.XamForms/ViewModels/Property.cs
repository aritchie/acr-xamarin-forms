using System;
using System.Reactive.Subjects;


namespace Acr.XamForms.ViewModels {
    
    public class Property<T> : AbstractNpcObject, IProperty<T> {
        private readonly ISubject<IProperty<T>> subject = new Subject<IProperty<T>>(); 
        private bool hasBeenBound;


        private T value;
        public T Value {
            get { return this.value; }
            set {
                this.SetProperty(ref this.value, value);
                if (this.hasBeenBound) 
                    this.subject.OnNext(this);
                else {
                    this.hasBeenBound = true;
                    this.OriginalValue = value;
                }
            }
        }


        public T OriginalValue { get; private set; }


        private string errorMessage;
        public string ErrorMessage {
            get { return this.errorMessage; }
            set { this.SetProperty(ref this.errorMessage, value); }
        }


        public virtual bool IsDirty {
            get { return !Object.Equals(this.Value, this.OriginalValue); }
        }


        public virtual void Reset() {
            this.Value = this.OriginalValue;
        }


        public IDisposable Subscribe(IObserver<IProperty<T>> observer) {
            return this.subject.Subscribe(observer);
        }
    }
}
