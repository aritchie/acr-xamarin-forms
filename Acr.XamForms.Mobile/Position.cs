using System;


namespace Acr.XamForms.Mobile {
    
    public class Position {

        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// 
        /// <value>
        /// The latitude.
        /// </value>
        /// 
        /// <remarks/>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// 
        /// <value>
        /// The longitude.
        /// </value>
        /// 
        /// <remarks/>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude in meters relative to sea level.
        /// </summary>
        /// 
        /// <value>
        /// The altitude in meters, relative to seal level.
        /// </value>
        /// 
        /// <remarks/>
        public double Altitude { get; set; }

        /// <summary>
        /// Gets or sets the potential position error radius in meters.
        /// </summary>
        /// 
        /// <value>
        /// The potential position error radius in meters of the <see cref="M:Xamarin.Geolocation.Position.Longitude"/> and <see cref="M:Xamarin.Geolocation.Position.Latitude"/> members.
        /// </value>
        /// 
        /// <remarks>
        /// This may be higher or lower than the <see cref="M:Xamarin.Geolocation.Geolocator.DesiredAccuracy"/>.
        /// </remarks>
        public double Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the potential altitude error range in meters.
        /// </summary>
        /// 
        /// <value>
        /// The potential error, in meters, of the <see cref="M:Xamarin.Geolocation.Position.Altitude"/> member.
        /// </value>
        /// 
        /// <remarks>
        /// Not currently supported on Android, will always read 0.
        /// </remarks>
        public double AltitudeAccuracy { get; set; }

        /// <summary>
        /// Gets or sets the heading in degrees relative to true North.
        /// </summary>
        /// 
        /// <value>
        /// Heading relative to true north.
        /// </value>
        /// 
        /// <remarks>
        /// Use <see cref="M:Xamarin.Geolocation.Geolocator.SupportsHeading"/> to determine whether this property will contain a value.
        /// </remarks>
        public double Heading { get; set; }

        /// <summary>
        /// Gets or sets the speed in meters per second.
        /// </summary>
        /// 
        /// <value>
        /// The speed of the device, in meters per second, when the position was recorded.
        /// </value>
        /// 
        /// <remarks>
        /// This value is only for the speed at the instant the position was recorded, as such it may vary wildly.
        /// </remarks>
        public double Speed { get; set; }
    }
}
