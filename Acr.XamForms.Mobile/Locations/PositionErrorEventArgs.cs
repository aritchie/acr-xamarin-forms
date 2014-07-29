using System;


namespace Acr.XamForms.Mobile.Locations {

    public enum GeoLocationError {
        PositionUnavailable,
        Unauthorized
    }
    

    public class PositionErrorEventArgs : EventArgs {
        public GeoLocationError Error { get; private set; }

        public PositionErrorEventArgs(GeoLocationError error) {
            this.Error = error;
        }
    }
}
