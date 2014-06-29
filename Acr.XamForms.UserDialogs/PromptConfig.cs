using System;


namespace Acr.XamForms.UserDialogs {
    
    public class PromptConfig {

        public string Title { get; set; }
        public string Message { get; set; }
        public Action<PromptResult> OnResult { get; set; }

        // TODO: can cancel
        public string OkText { get; set; }
        public string CancelText { get; set; }
        public string Placeholder { get; set; }
        public bool IsMultiline { get; set; }


        public PromptConfig() {
            this.OkText = "OK";
            this.CancelText = "Cancel";
        }
    }
}
