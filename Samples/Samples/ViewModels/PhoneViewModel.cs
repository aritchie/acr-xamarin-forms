using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class PhoneViewModel : ViewModel {

        public PhoneViewModel(IPhoneService phone) {
            this.Call = new Command(() => phone.Call(this.DisplayName, this.PhoneNumber));
        }


        public ICommand Call { get; private set; }


        private string displayName;
        public string DisplayName {
            get { return this.displayName; }
            set { this.SetProperty(ref this.displayName, value); }
        }


        private string phoneNumber;
        public string PhoneNumber {
            get { return this.phoneNumber; }
            set {
                if (value == null)
                    return;

                if (char.IsDigit(value, value.Length - 1) && value.Length <= 10)
                    this.phoneNumber = value;

                this.OnPropertyChanged();
            }
        }
    }
}
