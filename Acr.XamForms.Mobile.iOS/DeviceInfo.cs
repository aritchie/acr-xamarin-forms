using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;


namespace Acr.XamForms.Mobile.iOS {
    
    public class DeviceInfo : IDeviceInfo {

        public string AppVersion {
            get { return NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(); }
        }


        public int ScreenHeight {
            get { return (int)UIScreen.MainScreen.Bounds.Height; }
        }


        public int ScreenWidth {
            get { return (int)UIScreen.MainScreen.Bounds.Width; }
        }


        public string DeviceId {
            get { return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); }
        }


        public string Manufacturer {
            get { return "Apple"; }
        }


        public string Model {
            get { return UIDevice.CurrentDevice.Model; }
        }

        private string os;
        public string OperatingSystem {
            get {
                this.os = this.os ?? String.Format("{0} {1}", UIDevice.CurrentDevice.SystemName, UIDevice.CurrentDevice.SystemVersion);
                return this.os;
            }
        }

        public bool IsFrontCameraAvailable {
            get { return UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front); }
        }


        public bool IsRearCameraAvailable {
            get { return UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear); }
        }


        public bool IsSimulator {
            get { return (Runtime.Arch == Arch.SIMULATOR); }
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