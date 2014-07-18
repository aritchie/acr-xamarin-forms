//using System;


//namespace Acr.XamForms.UserDialogs {
    
//    public class DurationPromptConfig {
        
//        public Action<DurationPromptResult> OnResult { get; set; }

//        public string Title { get; set; }
//        public string OkText { get; set; }
//        public string CancelText { get; set; }
        
//        // can cancel?
//        // show seconds, mins, hours
//        // intervals of mins (1-100, 15, 30, 45, 60)
//        public TimeSpan? MinValue { get; set; }
//        public TimeSpan? MaxValue { get; set; }


//        public DurationPromptConfig() {
//            this.OkText = "OK";
//            this.CancelText = "Cancel";
//        }


//        public DurationPromptConfig SetTitle(string title) {
//            this.Title = title;
//            return this;
//        }


//        public DurationPromptConfig SetOkText(string text) {
//            this.OkText = text;
//            return this;
//        }


//        public DurationPromptConfig SetCancelText(string text) {
//            this.CancelText = text;
//            return this;
//        }


//        public DurationPromptConfig SetRange(TimeSpan? minValue, TimeSpan? maxValue) {
//            this.MinValue = minValue;
//            this.MaxValue = maxValue;
//            return this;
//        }


//        public DurationPromptConfig SetCallback(Action<DurationPromptResult> onResult) {
//            this.OnResult = onResult;
//            return this;
//        }
//    }
//}
