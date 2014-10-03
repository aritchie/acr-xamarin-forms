using System;


namespace Acr.XamForms.Mobile {
    
    public interface IDeviceInfo {

        int ScreenHeight { get; }
        int ScreenWidth { get; }
        string AppVersion { get; }
        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        bool IsFrontCameraAvailable { get; }
        bool IsRearCameraAvailable { get; }
        bool IsSimulator { get; }

        // IsFlashAvailable
        // Camera resolution?
        //BatteryState BatteryState { get; }
        //int BatteryPercentage { get; }
    }
}
