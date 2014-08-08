//using System;
//using System.ComponentModel;
//using System.Threading.Tasks;




//namespace Acr.XamForms.Mobile.Net {
    
//    public abstract class AbstractNetworkService : INetworkService {

//        public abstract Task<bool> IsHostReachable(string host);
//        public event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged;



//        private bool isInternetAvailable;
//        public bool IsInternetAvailable {
//            get { return this.isInternetAvailable; }
//            private set {
//                if (this.isInternetAvailable == value)
//                    return;

//                this.isInternetAvailable = value;
//                this.OnPropertyChanged("IsInternetAvailable");
//            }
//        }


//        protected virtual void OnNetworkStatusChanged() {
//            if (this.NetworkStatusChanged != null)
//                this.NetworkStatusChanged(this, new NetworkStatusChangedEventArgs(this.IsConnected, this.IsWifi, this.IsMobile));
//        }


//        public event PropertyChangedEventHandler PropertyChanged;
//        private void OnPropertyChanged(string propertyName) {
//            if (this.PropertyChanged != null) 
//                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
