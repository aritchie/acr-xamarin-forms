using System;


namespace Acr.XamForms.BarCodeScanner {

    public class BarCodeResult {

        public bool Success { get; private set; }
        public string Code { get; private set; }
        public BarCodeFormat Format { get; private set; }


        public static BarCodeResult Fail { get; private set; }

        static BarCodeResult() {
            Fail = new BarCodeResult {
                Success = false
            };
        }

        private BarCodeResult() { }

        public BarCodeResult(string code, BarCodeFormat format) {
            this.Success = true;
            this.Code = code;
            this.Format = format;
        }
    }
}