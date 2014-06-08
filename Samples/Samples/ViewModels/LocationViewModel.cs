using System;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class LocationViewModel : ViewModel {
        private readonly ILocationService location;


        public LocationViewModel(ILocationService location) {
            this.location = location;    
        }
    }
}
