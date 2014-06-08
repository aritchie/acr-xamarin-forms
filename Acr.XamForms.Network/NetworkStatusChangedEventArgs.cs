using System;


namespace Acr.XamForms.Network {
    
    public class NetworkStatusChangedEventArgs : EventArgs {

        public bool IsConnected { get; private set; }
        public bool IsWifi { get; private set; }
        public bool IsMobile { get; private set; }


        public NetworkStatusChangedEventArgs(bool connected, bool wifi, bool mobile) {
            this.IsConnected = connected;
            this.IsWifi = wifi;
            this.IsMobile = mobile;
        }
    }
}
