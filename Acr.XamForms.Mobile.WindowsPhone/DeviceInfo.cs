using System;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Env = System.Environment;
using DevEnv = Microsoft.Devices.Environment;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class DeviceInfo : AbstractDeviceInfo {
        
        public DeviceInfo() {
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
        }


        private static int GetScaleFactor()  {  
            var instance = Application.Current.Host.Content;
            var getMethod = instance.GetType().GetProperty("ScaleFactor").GetGetMethod();
            var value = (int)getMethod.Invoke(instance, null);
            return value;
        }
    }
}