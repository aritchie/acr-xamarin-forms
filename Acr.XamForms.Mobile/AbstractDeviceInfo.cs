using System;


namespace Acr.XamForms.Mobile {
    
    public abstract class AbstractDeviceInfo : IDeviceInfo {

        public int ScreenHeight { get; protected set; }
        public int ScreenWidth { get; protected set; }
        public string DeviceId { get; protected set; }
        public string Manufacturer { get; protected set; }
        public string Model { get; protected set; }
        public string OperatingSystem { get; protected set; }
        public bool IsSimulator { get; protected set; }
        public bool IsFrontCameraAvailable { get; protected set; }
        public bool IsRearCameraAvailable { get; protected set; }

        //public int BatteryPercentage { get; protected set; }
        //public BatteryState BatteryState { get; protected set; }
    }
}
