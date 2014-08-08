//using System;
//using System.Threading.Tasks;
//using Acr.XamForms.Mobile.Net;
//using Android.App;
//using Android.Net;
//using Java.Net;


//namespace Acr.XamForms.Mobile.Droid.Net {
    
//    public class NetworkService : AbstractNetworkService {

//        public NetworkService() {
//            NetworkConnectionBroadcastReceiver.OnChange = this.SetFromInfo;
//            var manager = (ConnectivityManager)Application.Context.GetSystemService(Application.ConnectivityService);
//            this.SetFromInfo(manager.ActiveNetworkInfo);
//        }


//        private void SetFromInfo(NetworkInfo network) {
//            //network.Subtype == ConnectivityType.Wifi
//            this.SetStatus(
//                (network != null && network.IsConnected),
//                (network != null && network.Type == ConnectivityType.Wifi),
//                (network != null && network.Type == ConnectivityType.Mobile)
//            );
//        }


//        public override Task<bool> IsHostReachable(string host) {
//            return Task<bool>.Run(() => {
//                try {
//                    return InetAddress
//                        .GetByName(host)
//                        .IsReachable(5000);
//                }
//                catch {
//                    return false;
//                }
//            });
//        }
//    }
//}
