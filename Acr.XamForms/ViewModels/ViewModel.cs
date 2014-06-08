using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Acr.XamForms.ViewModels {
    
    public abstract class ViewModel : IViewModel {

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null){
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


        //protected virtual void NavigateTo() {
        //    var nav = DependencyService.Get<INavigation>();

        //}


        public void Init(object args) {
        }


        public virtual Task Start() {
            return Task.FromResult(0);
        }
    }
}
