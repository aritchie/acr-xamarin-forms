using System;
using System.ComponentModel;


namespace Acr.XamForms.ViewModels {
    
    public interface IViewModel : INotifyPropertyChanged {

        void Init(object args);
        void OnAppearing();
        void OnDisappearing();
    }
}
