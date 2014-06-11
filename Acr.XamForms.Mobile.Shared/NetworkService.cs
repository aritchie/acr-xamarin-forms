using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
#if __ANDROID__
using Acr.XamForms.Mobile.Droid;
using Android.App;
using Android.Net;
using Java.Net;
#elif __IOS__
using Acr.XamForms.Mobile.iOS;
#else
using Microsoft.Phone.Net.NetworkInformation;
#endif



namespace Acr.XamForms.Mobile {
    
    public class NetworkService : INetworkService {

#if __ANDROID__
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


        public Task<bool> IsHostReachable(string host) {
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
#elif __IOS__
        public NetworkService() {
            this.SetInfo();
            Reachability.ReachabilityChanged += (s, e) => this.SetInfo();
        }


        public Task<bool> IsHostReachable(string host) {
            return Task<bool>.Run(() => Reachability.IsHostReachable(host));
        }


        private void SetInfo() {
            switch (Reachability.InternetConnectionStatus()) {
                case NetworkStatus.NotReachable:
                    this.SetStatus(false, false, false);
                    break;

                case NetworkStatus.ReachableViaCarrierDataNetwork:
                    this.SetStatus(true, false, true);
                    break;

                case NetworkStatus.ReachableViaWiFiNetwork:
                    this.SetStatus(true, false, true);
                    break;
            }     
        }
#else
        public NetworkService() {
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


        public Task<bool> IsHostReachable(string host) {
            // TODO
            return null;
        }
#endif

        private void SetStatus(bool connected, bool wifi, bool mobile) {
            this.IsConnected = connected;
            this.IsWifi = wifi;
            this.IsMobile = mobile;
            this.OnNetworkStatusChanged();
        }

        #region INetworkService Members
        
        public event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged;


        private bool connected;
        public bool IsConnected {
            get { return this.connected; }
            private set {
                if (this.connected == value)
                    return;

                this.connected = value;
                this.OnPropertyChanged("IsConnected");
            }
        }
        
        
        private bool wifi;
        public bool IsWifi {
            get { return this.wifi; }
            private set {
                if (this.wifi == value)
                    return;

                this.wifi = value;
                this.OnPropertyChanged("IsWifi");
            }
        }


        private bool mobile;
        public bool IsMobile {
            get { return this.mobile; }
            private set {
                if (this.mobile == value)
                    return;

                this.mobile = value;
                this.OnPropertyChanged("IsMobile");
            }
        }


        protected virtual void OnNetworkStatusChanged() {
            if (this.NetworkStatusChanged != null)
                this.NetworkStatusChanged(this, new NetworkStatusChangedEventArgs(this.IsConnected, this.IsWifi, this.IsMobile));
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
