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

        public Action<DateTimePromptResult> OnResult { get; set; }
        //public int MinuteIntervals { get; set; }
        //public bool ShowSeconds { get; set; }

        public DateTimePromptConfig() {
            this.OkText = "OK";
            this.CancelText = "Cancel";
        }


        public DateTimePromptConfig SetTitle(string title) {
            this.Title = title;
            return this;
        }


        public DateTimePromptConfig SetOkText(string text) {
            this.OkText = text;
            return this;
        }


        public DateTimePromptConfig SetCancelText(string text) {
            this.CancelText = text;
            return this;
        }


        public DateTimePromptConfig SetRange(DateTime? minValue, DateTime? maxValue) {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            return this;
        }


        public DateTimePromptConfig SetCallback(Action<DateTimePromptResult> onResult) {
            this.OnResult = onResult;
            return this;
        }
    }
}
