using System;
using System.Threading.Tasks;
using Acr.XamForms.Network.Droid;
using Android.App;
using Android.Net;
using Java.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidNetworkService))]


namespace Acr.XamForms.Network.Droid {
    
    public class DroidNetworkService : AbstractNetworkService {
        
        public DroidNetworkService() {
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