using System;
using System.ComponentModel;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile.Net {

    public interface INetworkService : INotifyPropertyChanged {
        
        Task<bool> IsHostReachable(string host);
        string IpAddress { get; }
        bool IsConnected { get; }
        bool IsWifi { get; }
        bool IsMobile { get; }
        bool IsRoaming { get; }

        event EventHandler StatusChanged; 
    }
}