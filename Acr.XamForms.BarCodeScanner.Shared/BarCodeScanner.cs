using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.XamForms.BarCodeScanner;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

[assembly: Dependency(typeof(BarCodeScanner))]


namespace Acr.XamForms.BarCodeScanner {
    

    public class BarCodeScanner : IBarCodeScanner {

        public BarCodeScannerOptions DefaultOptions { get; private set; }


        public BarCodeScanner() {
            this.DefaultOptions = new BarCodeScannerOptions();
        }


        public void Read(Action<BarCodeResult> onRead, Action<Exception> onError, BarCodeScannerOptions options) {
            this.ReadAsync(options)
                .ContinueWith(x => {
                    if (x.Exception == null)
                        onRead(x.Result);
                    else if (onError != null)
                        onError(x.Exception);
                });
        }


        public async Task<BarCodeResult> ReadAsync(BarCodeScannerOptions options) {
#if __IOS__
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false };
#elif ANDROID
            var scanner = new MobileBarcodeScanner(Forms.Context) { UseCustomOverlay = false };
#elif WINDOWS_PHONE
            var scanner = new MobileBarcodeScanner(System.Windows.Deployment.Current.Dispatcher) { UseCustomOverlay = false };
#endif
            options = options ?? this.DefaultOptions;
            if (!String.IsNullOrWhiteSpace(options.TopText)) 
                scanner.TopText = options.TopText;
            
            if (!String.IsNullOrWhiteSpace(options.BottomText)) 
                scanner.BottomText = options.BottomText;
            
            if (!String.IsNullOrWhiteSpace(options.FlashlightText)) 
                scanner.FlashButtonText = options.FlashlightText;
            
            if (!String.IsNullOrWhiteSpace(options.CancelText)) 
                scanner.CancelButtonText = options.CancelText;

            var cfg = GetXingConfig(options);
            var result = await scanner.Scan(cfg);
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, FromXingFormat(result.BarcodeFormat))
            );
        }


        private static BarCodeFormat FromXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


        private static MobileBarcodeScanningOptions GetXingConfig(BarCodeScannerOptions opts) {
            var def = ZXing.Mobile.MobileBarcodeScanningOptions.Default;

            var config = new MobileBarcodeScanningOptions {
                AutoRotate = def.AutoRotate,
                CharacterSet = opts.CharacterSet ?? def.CharacterSet,
                DelayBetweenAnalyzingFrames = opts.DelayBetweenAnalyzingFrames ?? def.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = (opts.InitialDelayBeforeAnalyzingFrames ?? def.InitialDelayBeforeAnalyzingFrames),
                PureBarcode = opts.PureBarcode,
                TryHarder = opts.TryHarder,
                TryInverted = opts.TryInverted,
                UseFrontCameraIfAvailable = opts.UseFrontCameraIfAvailable
            };
            if (opts.Formats != null && opts.Formats.Count > 0) {
                config.PossibleFormats = opts.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return config;
        }
    }
}
