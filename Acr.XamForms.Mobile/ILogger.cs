using System;


namespace Acr.XamForms.Mobile {
    
    public interface ILogger {

        void Debug(string tag, string message, params object[] args);

        void Error(string tag, Exception ex);
        void Error(string tag, string message, params object[] args);
    }
}
