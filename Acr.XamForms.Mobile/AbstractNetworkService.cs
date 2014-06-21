using System;
using System.ComponentModel;
using System.Threading.Tasks;




namespace Acr.XamForms.Mobile {
    
    public abstract class AbstractNetworkService : INetworkService {

        public abstract Task<bool> IsHostReachable(string host);

        protected void SetStatus(bool connected, bool wifi, bool mobile) {
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
            if (this.PropertyChanged != null) 
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
