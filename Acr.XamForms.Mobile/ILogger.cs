using System;


namespace Acr.XamForms.Mobile {
    
    public interface ILogger {

        void Trace(string tag, string message);
        void Error(string tag, string message, Exception ex);
    }
}
