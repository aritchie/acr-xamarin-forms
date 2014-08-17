using System;
using System.ComponentModel;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile.Net {
    
    public abstract class AbstractNetworkService : INetworkService {

        public abstract Task<bool> IsHostReachable(string host);

        public event EventHandler StatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsConnected { get; protected set; }
        public bool IsWifi { get; protected set; }
        public bool IsMobile { get; protected set; }
        public bool IsRoaming { get; protected set; }


        protected void PostUpdateStates() {
            if (!this.IsConnected) {
                this.IsWifi = false;
                this.IsMobile = false;
                this.IsRoaming = false;
            }
            this.OnPropertyChanged("IsWifi");
            this.OnPropertyChanged("IsMobile");
            this.OnPropertyChanged("IsRoaming");
            this.OnPropertyChanged("IsConnected");
            this.OnStatusChanged();
        }


        protected virtual void OnStatusChanged() {
            if (this.StatusChanged != null)
                this.StatusChanged(this, EventArgs.Empty);
        }


        protected virtual void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) 
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
