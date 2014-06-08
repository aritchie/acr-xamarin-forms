using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class PhoneViewModel : ViewModel {

        public PhoneViewModel(IPhoneService phone) {
            this.Call = new Command(() => phone.Call(String.Empty, this.PhoneNumber));
        }



        public ICommand Call { get; private set; }


        private string phoneNumber;
        public string PhoneNumber {
            get { return this.phoneNumber; }
            set { this.SetProperty(ref this.phoneNumber, value); }
        }
    }
}
