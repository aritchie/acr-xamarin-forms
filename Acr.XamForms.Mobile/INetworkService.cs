using System;
using System.ComponentModel;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile {

    public interface INetworkService : INotifyPropertyChanged {

        bool IsConnected { get; }
        bool IsWifi { get; }
        bool IsMobile { get; }
        Task<bool> IsHostReachable(string host);
        event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged; 
    }
}
