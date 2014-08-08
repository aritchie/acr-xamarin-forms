using System;


namespace Acr.XamForms.ViewModels {
    
    public abstract class ViewModel : AbstractNpcObject, IViewModel {

        public bool IsVisible { get; private set; }
        private bool started = false;


        public virtual void Init(object args) {
        }


        protected virtual void OnStart() {
        }


        public virtual void OnAppearing() {
            this.IsVisible = true;
            if (!this.started) {
                this.OnStart();
                this.started = true;
            }
        }


        public virtual void OnDisappearing() {
            this.IsVisible = false;
        }
    }
}
