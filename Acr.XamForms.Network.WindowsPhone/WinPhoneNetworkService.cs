using System;
using System.Threading.Tasks;
using Acr.XamForms.Network.WindowsPhone;
using Microsoft.Phone.Net.NetworkInformation;
using Xamarin.Forms;


[assembly: Dependency(typeof(WinPhoneNetworkService))]


namespace Acr.XamForms.Network.WindowsPhone {
    
    public class WinPhoneNetworkService : AbstractNetworkService {

        public WinPhoneNetworkService() {
            DeviceNetworkInformation.NetworkAvailabilityChanged += this.OnNetworkAvailabilityChanged;
            this.SetStatus(
                DeviceNetworkInformation.IsNetworkAvailable,
                DeviceNetworkInformation.IsWiFiEnabled,
                DeviceNetworkInformation.IsCellularDataEnabled
            );
        }


        private void OnNetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e) {
            this.SetStatus(
                DeviceNetworkInformation.IsNetworkAvailable,
                DeviceNetworkInformation.IsWiFiEnabled,
                DeviceNetworkInformation.IsCellularDataEnabled
            );
        }


        public override Task<bool> IsHostReachable(string host) {
            // TODO
            return null;
        }
    }
}
