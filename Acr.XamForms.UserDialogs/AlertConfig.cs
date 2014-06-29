using System;


namespace Acr.XamForms.UserDialogs {
    
    public class AlertConfig {

        public string OkText { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Action OnOk { get; set; }


        public AlertConfig() {
            this.OkText = "OK";
        }
    }
}
