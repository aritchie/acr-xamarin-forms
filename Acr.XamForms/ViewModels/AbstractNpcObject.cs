using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Acr.XamForms.ViewModels {
    
    public abstract class AbstractNpcObject : INotifyPropertyChanged {
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
