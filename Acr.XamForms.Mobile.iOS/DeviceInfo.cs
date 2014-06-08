using System;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;


namespace Acr.XamForms.Mobile.iOS {
    
    public class DeviceInfo : AbstractDeviceInfo {
        
        public DeviceInfo() {
            this.Manufacturer = "Apple";
            this.Model = UIDevice.CurrentDevice.Model;
            this.OperatingSystem = String.Format("{0} {1}", UIDevice.CurrentDevice.SystemName, UIDevice.CurrentDevice.SystemVersion);
            this.DeviceId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();

            var screen = UIScreen.MainScreen.Bounds;
            this.ScreenWidth = (int)screen.Width;
            this.ScreenHeight = (int)screen.Height;
            this.IsFrontCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
            this.IsRearCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);
            this.IsSimulator = (Runtime.Arch == Arch.SIMULATOR);
            
            //this.BatteryPercentage = UIDevice.CurrentDevice.BatteryLevel;
        }
    }
}
/*
UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
// set the battery level on the progress bar
barBatteryLevel.Progress = UIDevice.CurrentDevice.BatteryLevel;
// the the battery state label
lblBatteryState.Text = UIDevice.CurrentDevice.BatteryState.ToString ();

// add a notification handler for battery level changes
NSNotificationCenter.DefaultCenter.AddObserver (
    UIDevice.BatteryLevelDidChangeNotification,
    (NSNotification n) => { 
        barBatteryLevel.Progress = UIDevice.CurrentDevice.BatteryLevel;
        n.Dispose();
    });

// add a notification handler for battery state changes
NSNotificationCenter.DefaultCenter.AddObserver (
    UIDevice.BatteryStateDidChangeNotification,
    (NSNotification n) => { 
        lblBatteryState.Text = UIDevice.CurrentDevice.BatteryState.ToString(); 
        n.Dispose();
    });
 */