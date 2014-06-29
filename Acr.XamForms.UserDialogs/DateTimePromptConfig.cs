using System;


namespace Acr.XamForms.UserDialogs {

    public enum DateSelectionType {
        Date,
        Time,
        DateTime
    }


    public class DateTimePromptConfig {

        public string Title { get; set; }
        public string OkText { get; set; }
        public string CancelText { get; set; }
        // TODO: show cancel?

        public DateTime? MinValue { get; set; }
        public DateTime? MaxValue { get; set; }

        public Action<DateTimePromptResult> OnResult { get; private set; }
        //TODO
        //fluent methods
        //public int MinuteIntervals { get; set; }
        //public bool ShowSeconds { get; set; }

        public DateTimePromptConfig(Action<DateTimePromptResult> onResult) {
            this.OnResult = onResult;
            this.OkText = "OK";
            this.CancelText = "Cancel";
        }
    }
}
