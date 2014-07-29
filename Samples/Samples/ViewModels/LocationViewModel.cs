using System;
using Acr.XamForms.Mobile.Locations;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class LocationViewModel : ViewModel {
        private readonly IGeoLocator location;


        public LocationViewModel(IGeoLocator location) {
            this.location = location;
        }


        public override void OnAppearing() {
            this.location.PositionChanged += this.OnPositionChanged;
            this.location.StartListening(1, 10, true);
        }


        public override void OnDisappearing() {
            this.location.PositionChanged -= this.OnPositionChanged;
            this.location.StopListening();
        }


        private void OnPositionChanged(object sender, PositionEventArgs e) {
            this.Latitude = e.Position.Latitude;
            this.Longitude = e.Position.Longitude;
            this.Altitude = e.Position.Altitude;
            this.Heading = e.Position.Heading;
        }

        
        private double latitude;
        public double Latitude {
            get { return this.latitude; }
            private set { this.SetProperty(ref this.latitude, value); }
        }


        private double longitude;
        public double Longitude {
            get { return this.longitude; }
            private set { this.SetProperty(ref this.longitude, value); }
        }


        private double altitude;
        public double Altitude {
            get { return this.altitude; }
            private set { this.SetProperty(ref this.altitude, value); }
        }


        private double heading;
        public double Heading {
            get { return this.heading; }
            private set { this.SetProperty(ref this.heading, value); }
        }
    }
}