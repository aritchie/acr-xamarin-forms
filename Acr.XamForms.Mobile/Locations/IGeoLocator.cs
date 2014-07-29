using System;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile.Locations {
    
    public interface IGeoLocator {

        double DesiredAccuracy { get; set; }
        bool IsListening { get; }
        bool IsGeoLocationAvailable { get; }
        bool SupportsHeading { get; }

        void StartListening(int minTime, double minDistance, bool includeHeading = false);
        void StopListening();

        Task<Position> GetPositionAsync(int timeout = 30, bool includeHeading = false, CancellationToken cancelToken = default(CancellationToken));
        event EventHandler<PositionEventArgs> PositionChanged;
        event EventHandler<PositionErrorEventArgs> PositionError; 
    }
}