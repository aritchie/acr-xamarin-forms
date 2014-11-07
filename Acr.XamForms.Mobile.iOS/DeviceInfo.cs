using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;


namespace Acr.XamForms.Mobile.iOS {
    
    public class DeviceInfo : IDeviceInfo {

		public DeviceInfo() {
			this.AppVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
			this.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
			this.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
			this.DeviceId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
			this.Manufacturer = "Apple";
			this.Model = UIDevice.CurrentDevice.Model;
			this.OperatingSystem = String.Format("{0} {1}", UIDevice.CurrentDevice.SystemName, UIDevice.CurrentDevice.SystemVersion);
			this.IsFrontCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
			this.IsRearCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);
			this.IsSimulator = (Runtime.Arch == Arch.SIMULATOR);
		}


		public string AppVersion { get; private set; }
		public int ScreenHeight { get; private set; }
		public int ScreenWidth { get; private set; }
		public string DeviceId { get; private set; }
		public string Manufacturer { get; private set; }
		public string Model { get; private set; }
		public string OperatingSystem { get; private set; }
		public bool IsFrontCameraAvailable { get; private set; }
		public bool IsRearCameraAvailable { get; private set; }
		public bool IsSimulator { get; private set; }
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