using System;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Env = System.Environment;
using DevEnv = Microsoft.Devices.Environment;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class DeviceInfo : IDeviceInfo {

        private readonly Lazy<string> deviceId;


        public DeviceInfo() {
            this.deviceId = new Lazy<string>(() => {
                var deviceIdBytes = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
                return Convert.ToBase64String(deviceIdBytes);
            });
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


        public int ScreenHeight { get; private set; }
        public int ScreenWidth { get; private set; }


        public string DeviceId {
            get { return this.deviceId.Value; }
        }


        public string Manufacturer {
            get { return DeviceStatus.DeviceManufacturer; }
        }


        public string Model {
            get { return DeviceStatus.DeviceName; }
        }


        public string OperatingSystem {
            get { return Env.OSVersion.ToString(); }
        }


        public bool IsFrontCameraAvailable {
            get { return PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing); }
        }


        public bool IsRearCameraAvailable {
            get { return PhotoCamera.IsCameraTypeSupported(CameraType.Primary); }
        }


        public bool IsSimulator {
            get { return (DevEnv.DeviceType == DeviceType.Emulator); }
        }
    }
}
