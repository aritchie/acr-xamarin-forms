using System;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile {
    
    public interface ILocationService {

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
/*
using Xamarin.Geolocation;
// ...

var locator = new Geolocator { DesiredAccuracy = 50 };
//            new Geolocator (this) { ... }; on Android

Position position = await locator.GetPositionAsync (timeout: 10000);

Console.WriteLine ("Position Status: {0}", position.Timestamp);
Console.WriteLine ("Position Latitude: {0}", position.Latitude);
Console.WriteLine ("Position Longitude: {0}", position.Longitude);*/