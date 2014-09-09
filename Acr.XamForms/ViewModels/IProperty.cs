using System;
using System.ComponentModel;


namespace Acr.XamForms.ViewModels {
    
    public interface IProperty<T> : INotifyPropertyChanged, IObservable<IProperty<T>> {

        T Value { get; set; }
        T OriginalValue { get; }
        string ErrorMessage { get; set; }
        bool IsDirty { get; }
        void Reset();
    }
}
