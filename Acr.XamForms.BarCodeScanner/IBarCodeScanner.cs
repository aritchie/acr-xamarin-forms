using System;
using System.Threading.Tasks;


namespace Acr.XamForms.BarCodeScanner {

    public interface IBarCodeScanner {

        BarCodeScannerConfiguration Configuration { get; }
        
        void Read(Action<BarCodeResult> onRead);
        Task<BarCodeResult> ReadAsync();
    }
}
