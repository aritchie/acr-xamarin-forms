using System;
using System.ComponentModel;
using System.Threading.Tasks;


namespace Acr.XamForms.ViewModels {
    
    public interface IViewModel : INotifyPropertyChanged {

        void Init(object args);
        Task Start();
    }
}
