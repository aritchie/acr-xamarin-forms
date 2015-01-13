using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Acr.XamForms.Mobile.Net;
using Acr.XamForms.Mobile.WindowsPhone.Net;
using Microsoft.Phone.Net.NetworkInformation;
using Xamarin.Forms;

[assembly: Dependency(typeof(NetworkService))]


namespace Acr.XamForms.Mobile.WindowsPhone.Net {

    public class NetworkService : AbstractNetworkService {

        public NetworkService() {
            this.UpdateStatus();
            DeviceNetworkInformation.NetworkAvailabilityChanged += this.OnNetworkAvailabilityChanged;
            NetworkChange.NetworkAddressChanged += (sender, args) => {}; // this has to be listened to as well to hear previous event
        }


        private void OnNetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e) {
            this.UpdateStatus();
        }


        public override Task<bool> IsHostReachable(string host) {
            var tcs = new TaskCompletionSource<bool>();
            DeviceNetworkInformation.ResolveHostNameAsync(new DnsEndPoint(host, 80), x => tcs.SetResult(x.HostName != null), null);
            return tcs.Task;
        }


        private void UpdateStatus() {
            this.IsConnected = DeviceNetworkInformation.IsNetworkAvailable;
            this.IsRoaming = DeviceNetworkInformation.IsCellularDataRoamingEnabled;
            this.IsMobile = DeviceNetworkInformation.IsCellularDataEnabled;
            this.IsWifi = DeviceNetworkInformation.IsWiFiEnabled;

            this.PostUpdateStates();
        }
    }
}
