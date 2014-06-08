using System;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class DeviceInfoViewModel : ViewModel {

        public IDeviceInfo Device { get; private set; }

        public DeviceInfoViewModel(IDeviceInfo deviceInfo) {
            this.Device = deviceInfo;
        }
    }
}
