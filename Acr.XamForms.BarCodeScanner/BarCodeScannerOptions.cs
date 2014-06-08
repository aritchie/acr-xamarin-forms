using System;
using System.Collections.Generic;


namespace Acr.XamForms.BarCodeScanner {
    
    public class BarCodeScannerOptions {

        public string TopText { get; set; }
        public string BottomText { get; set; }
        public string FlashlightText { get; set; }
        public string CancelText { get; set; }

        public bool? AutoRotate { get; set; }
        public string CharacterSet { get; set; }
        public int? DelayBetweenAnalyzingFrames { get; set; }
        public bool? PureBarcode { get; set; }
        public int? InitialDelayBeforeAnalyzingFrames { get; set; }
        public bool? TryHarder { get; set; }
        public bool? TryInverted { get; set; }
        public bool? UseFrontCameraIfAvailable { get; set; }

        public List<BarCodeFormat> Formats { get; set; }

        public BarCodeScannerOptions() {
            this.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
            this.BottomText = "Wait for the barcode to automatically scan";
            this.Formats = new List<BarCodeFormat>(3);
        }
    }
}