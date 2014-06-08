using System;
using System.Threading.Tasks;
using Android.App;
using Android.Net;
using Java.Net;
using Xamarin.Forms;


namespace Acr.XamForms.Mobile.Droid {
    
    public class NetworkService : AbstractNetworkService {
        
        public NetworkService() {
            NetworkConnectionBroadcastReceiver.OnChange = this.SetFromInfo;
            var manager = (ConnectivityManager)Forms.Context.ApplicationContext.GetSystemService(Application.ConnectivityService);
            this.SetFromInfo(manager.ActiveNetworkInfo);
        }


        private void SetFromInfo(NetworkInfo network) {
            this.SetStatus(
                network.IsConnected,
                (network != null && network.Type == ConnectivityType.Wifi),
                (network != null && network.Type == ConnectivityType.Mobile)
            );
        }


        public override Task<bool> IsHostReachable(string host) {
            return Task<bool>.Run(() => {
                try {
                    return InetAddress
                        .GetByName(host)
                        .IsReachable(5000);
                }
                catch {
                    return false;
                }
            });
        }
    }
}