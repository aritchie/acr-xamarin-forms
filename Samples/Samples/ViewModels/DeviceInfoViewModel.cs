using System;
using Acr.XamForms.DeviceInfo;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class DeviceInfoViewModel : ViewModel {

        public IDeviceInfoService Device { get; private set; }

        public DeviceInfoViewModel(IDeviceInfoService deviceInfo) {
            this.Device = deviceInfo;
        }
    }
}
