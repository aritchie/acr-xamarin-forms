using System;


namespace Acr.XamForms.UserDialogs {
    
    public class ConfirmConfig {

        public string Title { get; set; }
        public string Message { get; set; }
        public Action<bool> OnConfirm { get; set; }

        public string OkText { get; set; }
        public string CancelText { get; set; }
    }
}
