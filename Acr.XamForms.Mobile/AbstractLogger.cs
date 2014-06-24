using System;
using System.Linq;


namespace Acr.XamForms.Mobile {
    
    public abstract class AbstractLogger : ILogger {

        public void Debug(string tag, string message, params object[] args) {
            if (args != null && args.Any())
                message = String.Format(message, args);

            this.NativeDebug(tag, message);
        }


        public void Error(string tag, Exception ex) {
            this.NativeError(tag, "EXCEPTION - " + ex);
        }


        public void Error(string tag, string message, params object[] args) {
            if (args != null && args.Any())
                message = String.Format(message, args);

            this.NativeError(tag, message);
        }



        protected abstract void NativeDebug(string tag, string message);
        protected abstract void NativeError(string tag, string message);
    }
}
