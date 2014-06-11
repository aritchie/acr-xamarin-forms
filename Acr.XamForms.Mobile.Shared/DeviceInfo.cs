using System;
using Xamarin.Forms;

#if __ANDROID__
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Telephony;
using B = Android.OS.Build;
#elif __IOS__
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#elif WINDOWS_PHONE
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Env = System.Environment;
using DevEnv = Microsoft.Devices.Environment;
#endif


namespace Acr.XamForms.Mobile {
    
    public class DeviceInfo : IDeviceInfo {


        public int ScreenHeight { get; private set; }
        public int ScreenWidth { get; private set; }
        public string DeviceId { get; private set; }
        public string Manufacturer { get; private set; }
        public string Model { get; private set; }
        public string OperatingSystem { get; private set; }
        public bool IsSimulator { get; private set; }
        public bool IsFrontCameraAvailable { get; private set; }
        public bool IsRearCameraAvailable { get; private set; }

        //public int BatteryPercentage { get; private set; }
        //public BatteryState BatteryState { get; private set; }

        public DeviceInfo() {
#if __IOS__
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
#elif __ANDROID__
            this.Manufacturer = B.Manufacturer;
            this.Model = B.Model;
            this.OperatingSystem = String.Format("{0} - SDK: {1}", B.VERSION.Release, B.VERSION.SdkInt);
            this.IsSimulator = B.Product.Equals("google_sdk");

            var d = Resources.System.DisplayMetrics;
            this.ScreenWidth = (int)(d.WidthPixels / d.Density);
            this.ScreenHeight = (int)(d.HeightPixels / d.Density);

            var pm = Forms.Context.ApplicationContext.PackageManager;

            this.IsRearCameraAvailable = pm.HasSystemFeature(PackageManager.FeatureCamera);
            this.IsFrontCameraAvailable = pm.HasSystemFeature(PackageManager.FeatureCameraFront);

            var tel = (TelephonyManager)Forms.Context.ApplicationContext.GetSystemService(Context.TelephonyService);
            this.DeviceId = tel.DeviceId;

            //var filter = new IntentFilter(Intent.ActionBatteryChanged);
            //var battery = RegisterReceiver(null, filter);
            //var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
            //var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);
            //this.BatteryPercentage = Convert.ToInt32(Math.Floor(level * 100D / scale));
#elif WINDOWS_PHONE
            this.Manufacturer = DeviceStatus.DeviceManufacturer; 
            this.Model = DeviceStatus.DeviceName;
            this.OperatingSystem = Env.OSVersion.ToString();

            var deviceIdBytes = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
            this.DeviceId = Convert.ToBase64String(deviceIdBytes);

            this.IsRearCameraAvailable = PhotoCamera.IsCameraTypeSupported(CameraType.Primary);
            this.IsFrontCameraAvailable = PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing);
            this.IsSimulator = (DevEnv.DeviceType == DeviceType.Emulator);

            switch (GetScaleFactor()) {               

                case 150:
                    this.ScreenWidth = 720;
                    this.ScreenHeight = 1280;
                    break;

                case 160:
                    this.ScreenWidth = 768;
                    this.ScreenHeight = 1280;
                    break;

                case 100:
                default:
                    this.ScreenWidth = 480;
                    this.ScreenHeight = 800;
                    break;
            }
#endif
        }

#if WINDOWS_PHONE

        private static int GetScaleFactor()  {  
            var instance = Application.Current.Host.Content;
            var getMethod = instance.GetType().GetProperty("ScaleFactor").GetGetMethod();
            var value = (int)getMethod.Invoke(instance, null);
            return value;
        }
#endif
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