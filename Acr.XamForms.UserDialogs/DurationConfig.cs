using System;


namespace Acr.XamForms.UserDialogs {
    
    public class DurationConfig {
        
        public Action<DurationPromptResult> OnResult { get; private set; }

        public string Title { get; set; }
        public string OkText { get; set; }
        public string CancelText { get; set; }
        
        // can cancel?
        public TimeSpan? MinValue { get; set; }
        public TimeSpan? MaxValue { get; set; }


        public DurationConfig(Action<DurationPromptResult> onResult) {
            this.OnResult = onResult;

            this.OkText = "OK";
            this.CancelText = "Cancel";
        }

        // TODO: fluent methods
    }
}
