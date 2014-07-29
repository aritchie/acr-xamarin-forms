using System;


namespace Acr.XamForms.Mobile.Media {

    public enum VideoQuality {
        High,
        Medium,
        Low
    }

    public class VideoOptions : AbstractOptions {

        public CameraDevice Camera { get; set; }
        public VideoQuality Quality { get; set; }
        public TimeSpan DesiredLength { get; set; }


        public VideoOptions() {
            this.Camera = CameraDevice.Rear;
            this.Quality = VideoQuality.High;
        }
    }
}
