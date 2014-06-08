using System;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
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
