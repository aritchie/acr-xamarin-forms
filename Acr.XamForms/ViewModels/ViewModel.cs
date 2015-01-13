using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Acr.XamForms.ViewModels {

    public abstract class ViewModel : IViewModel {

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


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null) {
            if (!Object.Equals(property, value)) {
                property = value;
                this.OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
