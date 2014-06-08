using System;
using System.Threading.Tasks;


namespace Acr.XamForms.BarCodeScanner {

    public interface IBarCodeScanner {

        BarCodeScannerOptions DefaultOptions { get; }
        
        void Read(Action<BarCodeResult> onRead, Action<Exception> onError = null, BarCodeScannerOptions options = null);
        Task<BarCodeResult> ReadAsync(BarCodeScannerOptions options = null);
    }
}
