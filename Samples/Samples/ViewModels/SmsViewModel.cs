using System;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class SmsViewModel : ViewModel {
        private readonly IPhoneService phone;


        public SmsViewModel(IPhoneService phone) {
            this.phone = phone;    
        }


    }
}
