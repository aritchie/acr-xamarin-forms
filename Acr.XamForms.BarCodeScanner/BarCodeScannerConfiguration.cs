using System;
using System.Collections.Generic;


namespace Acr.XamForms.BarCodeScanner {
    
    public class BarCodeScannerConfiguration {

        public string TopText { get; set; }
        public string BottomText { get; set; }
        public string FlashlightText { get; set; }
        public string CancelText { get; set; }

        public bool? AutoRotate { get; set; }
        public string CharacterSet { get; set; }
        public int DelayBetweenAnalyzingFrames { get; set; }
        public bool? PureBarcode { get; set; }
        public int InitialDelayBeforeAnalyzingFrames { get; set; }
        public bool? TryHarder { get; set; }
        public bool? TryInverted { get; set; }
        public bool? UseFrontCameraIfAvailable { get; set; }

        public List<BarCodeFormat> Formats { get; set; }
    }
}