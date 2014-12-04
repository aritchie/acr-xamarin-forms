using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Acr.XamForms.Mobile.iOS.Net;
using Acr.XamForms.Mobile.Net;
using Xamarin.Forms;


[assembly: Dependency(typeof(NetworkService))]


namespace Acr.XamForms.Mobile.iOS.Net {
    
    public class NetworkService : AbstractNetworkService {

        public NetworkService() {
            this.SetInfo();
            Reachability.ReachabilityChanged += (s, e) => this.SetInfo();
        }


        public override Task<bool> IsHostReachable(string host) {
            return Task<bool>.Run(() => Reachability.IsHostReachable(host));
        }


        public string GetIpAddress() {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Ethernet || x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                .SelectMany(x => x.GetIPProperties().UnicastAddresses)
                .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(x => x.Address.ToString())
                .FirstOrDefault();
        }


        private void SetInfo() {
            var state = Reachability.InternetConnectionStatus();
            switch (state) {
                case NetworkStatus.NotReachable:
                    this.IsConnected = false;
                    break;

                case NetworkStatus.ReachableViaCarrierDataNetwork:
                    this.IsConnected = true;
                    this.IsWifi = false;
                    this.IsMobile = true;
                    this.IsRoaming = false;
                    break;

                case NetworkStatus.ReachableViaWiFiNetwork:
                    this.IsConnected = true;
                    this.IsWifi = true;
                    this.IsMobile = false;
                    this.IsRoaming = false;
                    break;
            }
            this.PostUpdateStates();
        }
    }
}
