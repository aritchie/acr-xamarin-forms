using System;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class PhoneViewModel : ViewModel {
        private readonly IPhoneService phone;


        public PhoneViewModel(IPhoneService phone) {
            this.phone = phone;    
        }


    }
}
