using System;
using System.Linq;
using System.Threading.Tasks;


namespace Acr.XamForms.UserDialogs {
    
    public static class Extensions {

        #region Async Helpers

        public static Task AlertAsync(this IUserDialogService dialogs, string message, string title = null, string okText = "OK") {
            var tcs = new TaskCompletionSource<object>();
            dialogs.Alert(message, title, okText, () => tcs.SetResult(null));
            return tcs.Task;
        }


        public static Task AlertAsync(this IUserDialogService dialogs, Action<AlertConfig> action) {
            var tcs = new TaskCompletionSource<object>();
            dialogs.Alert(action);
            return tcs.Task;    
        }


        public static Task<bool> ConfirmAsync(this IUserDialogService dialogs, string message, string title = null, string okText = "OK", string cancelText = "Cancel") {
            var tcs = new TaskCompletionSource<bool>();
            dialogs.Confirm(message, tcs.SetResult, title, okText, cancelText);
            return tcs.Task;
        }


        public static Task<bool> ConfirmAsync(this IUserDialogService dialogs, Action<ConfirmConfig> action) {
            var tcs = new TaskCompletionSource<bool>();
            dialogs.Confirm(action);
            return tcs.Task;
        }


        public static Task<PromptResult> PromptAsync(this IUserDialogService dialogs, string message, string title = null, string okText = "OK", string cancelText = "Cancel", string placeholder = "", bool multiline = false) {
            var tcs = new TaskCompletionSource<PromptResult>();
            dialogs.Prompt(message, tcs.SetResult, title, okText, cancelText, placeholder, multiline);
            return tcs.Task;
        }


        public static Task<PromptResult> PromptAsync(this IUserDialogService dialogs, Action<PromptConfig> action) {
            var tcs = new TaskCompletionSource<PromptResult>();
            dialogs.Prompt(action);
            return tcs.Task;
        }


        //void DateTimePrompt(DateTimePromptConfig config);
        //void DurationPrompt(DurationConfig config);

        #endregion

        #region Fluent Helpers

        public static void ActionSheet(this IUserDialogService dialogs, Action<ActionSheetConfig> action) {
            var config = new ActionSheetConfig();
            action(config);
            dialogs.ActionSheet(config);
        }


        public static void Alert(this IUserDialogService dialogs, Action<AlertConfig> action) {
            var config = new AlertConfig();
            action(config);
            dialogs.Alert(config);
        }


        public static void Confirm(this IUserDialogService dialogs, Action<ConfirmConfig> action) {
            var config = new ConfirmConfig();
            action(config);
            dialogs.Confirm(config);
        }


        public static void Prompt(this IUserDialogService dialogs, Action<PromptConfig> action) {
            var config = new PromptConfig();
            action(config);
            dialogs.Prompt(config);
        }


        //void DateTimePrompt(DateTimePromptConfig config);
        //void DurationPrompt(DurationConfig config);

        #endregion

        #region Legacy Methods

        public static void ActionSheet(this IUserDialogService dialogs, string title = null, params ActionSheetOption[] options) {
            dialogs.ActionSheet(new ActionSheetConfig {
                Title = title,
                Options = options.ToList()
            });
        }


        public static void Alert(this IUserDialogService dialogs, string message, string title = null, string okText = "OK", Action onOk = null) {
            dialogs.Alert(new AlertConfig {
                Message = message,
                Title = title,
                OkText = okText,
                OnOk = onOk
            });
        }


        public static void Confirm(this IUserDialogService dialogs, string message, Action<bool> onConfirm, string title = null, string okText = "OK", string cancelText = "Cancel") {
            dialogs.Confirm(new ConfirmConfig {
                CancelText = cancelText,
                Message = message,
                OkText = okText,
                OnConfirm = onConfirm,
                Title = title
            });
        }


        public static void Prompt(this IUserDialogService dialogs, string message, Action<PromptResult> onResult, string title = null, string okText = "OK", string cancelText = "Cancel", string placeholder = null, bool multiline = false) {
            dialogs.Prompt(new PromptConfig {
                CancelText = cancelText,
                Message = message,
                OkText = okText,
                OnResult = onResult,
                Title = title,
                Placeholder = placeholder,
                IsMultiline = multiline
            });
        }


        public static IProgressDialog Loading(this IUserDialogService dialogs, string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true) {
            return dialogs.Progress(new ProgressConfig {
                Title = title,
                AutoShow = show,
                CancelText = cancelText,
                IsDeterministic = false,
                OnCancel = onCancel
            });
        }


        public static IProgressDialog Progress(this IUserDialogService dialogs, string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true) {
            return dialogs.Progress(new ProgressConfig {
                Title = title,
                AutoShow = show,
                CancelText = cancelText,
                IsDeterministic = true,
                OnCancel = onCancel
            });
        }

        #endregion

    }
}
