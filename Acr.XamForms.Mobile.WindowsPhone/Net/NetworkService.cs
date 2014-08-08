//using System;
//using System.Threading.Tasks;
//using Acr.XamForms.Mobile.Net;
//using Microsoft.Phone.Net.NetworkInformation;



//namespace Acr.XamForms.Mobile.WindowsPhone.Net {
    
//    public class NetworkService : AbstractNetworkService {

//        public NetworkService() {
//            DeviceNetworkInformation.NetworkAvailabilityChanged += this.OnNetworkAvailabilityChanged;
//            this.SetStatus(
//                DeviceNetworkInformation.IsNetworkAvailable,
//                DeviceNetworkInformation.IsWiFiEnabled,
//                DeviceNetworkInformation.IsCellularDataEnabled
//            );
//        }


//        private void OnNetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e) {
//            this.SetStatus(
//                DeviceNetworkInformation.IsNetworkAvailable,
//                DeviceNetworkInformation.IsWiFiEnabled,
//                DeviceNetworkInformation.IsCellularDataEnabled
//            );
//        }


//        public override Task<bool> IsHostReachable(string host) {
//            // TODO
//            return null;
//        }
//    }
//}
