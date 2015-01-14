﻿using System;
using System.Threading.Tasks;


namespace Acr.XamForms.UserDialogs {

    public abstract class AbstractUserDialogService : IUserDialogService {

        public abstract void Alert(AlertConfig config);
        public abstract void ActionSheet(ActionSheetConfig config);
        public abstract void Confirm(ConfirmConfig config);
        public abstract void Login(LoginConfig config);
        //public abstract void DateTimePrompt(DateTimePromptConfig config);
        //public abstract void DurationPrompt(DurationPromptConfig config);
        public abstract void Prompt(PromptConfig config);
        public abstract void Toast(string message, int timeoutSeconds = 3, Action onClick = null);
        public abstract void ShowNetworkLoading(string message = "Downloading");
        public abstract void HideNetworkLoading();
        public abstract IDownloadDialog NetworkLoading(string message = "Downloading");

        protected abstract IProgressDialog CreateDialogInstance();


        public virtual void Alert(string message, string title, string okText) {
            this.Alert(new AlertConfig {
                Message = message,
                Title = title,
                OkText = okText
            });
        }


        private IProgressDialog loading;

        public virtual void ShowLoading(string title) {
            if (this.loading == null)
                this.loading = this.Loading(title, null, null, true);
        }


        public virtual void HideLoading() {
            if (this.loading != null) {
                this.loading.Dispose();
                this.loading = null;
            }
        }


        public virtual IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show) {
            return this.Progress(new ProgressConfig {
                Title = title,
                AutoShow = show,
                CancelText = cancelText,
                IsDeterministic = false,
                OnCancel = onCancel
            });
        }


        public virtual IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            return this.Progress(new ProgressConfig {
                Title = title,
                AutoShow = show,
                CancelText = cancelText,
                IsDeterministic = true,
                OnCancel = onCancel
            });
        }


        public virtual IProgressDialog Progress(ProgressConfig config) {
            var dlg = this.CreateDialogInstance();
            dlg.Title = config.Title;
            dlg.IsDeterministic = config.IsDeterministic;

            if (config.OnCancel != null)
                dlg.SetCancel(config.OnCancel, config.CancelText);

            if (config.AutoShow)
                dlg.Show();

            return dlg;
        }


        public virtual Task AlertAsync(string message, string title, string okText) {
            var tcs = new TaskCompletionSource<object>();
            this.Alert(new AlertConfig {
                Message = message,
                Title = title,
                OkText = okText,
                OnOk = () => tcs.TrySetResult(null)
            });
            return tcs.Task;
        }


        public virtual Task AlertAsync(AlertConfig config) {
            var tcs = new TaskCompletionSource<object>();
            config.OnOk = () => tcs.TrySetResult(null);
            this.Alert(config);
            return tcs.Task;
        }


        public virtual Task<bool> ConfirmAsync(string message, string title, string okText, string cancelText) {
            var tcs = new TaskCompletionSource<bool>();
            this.Confirm(new ConfirmConfig {
                Message = message,
                CancelText = cancelText,
                OkText = okText,
                OnConfirm = x => tcs.TrySetResult(x)
            });
            return tcs.Task;
        }


        public virtual Task<bool> ConfirmAsync(ConfirmConfig config) {
            var tcs = new TaskCompletionSource<bool>();
            config.OnConfirm = x => tcs.TrySetResult(x);
            this.Confirm(config);
            return tcs.Task;
        }


        public virtual Task<LoginResult> LoginAsync(string title, string message) {
            return this.LoginAsync(new LoginConfig {
                Title = title,
                Message = message
            });
        }


        public virtual Task<LoginResult> LoginAsync(LoginConfig config) {
            var tcs = new TaskCompletionSource<LoginResult>();
            config.OnResult = x => tcs.TrySetResult(x);
            this.Login(config);
            return tcs.Task;
        }


        public virtual Task<PromptResult> PromptAsync(string message, string title, string okText, string cancelText, string placeholder, bool secure) {
            var tcs = new TaskCompletionSource<PromptResult>();
            this.Prompt(new PromptConfig {
                Message = message,
                Title = title,
                CancelText = cancelText,
                OkText = okText,
                Placeholder = placeholder,
                IsSecure = secure,
                OnResult = x => tcs.TrySetResult(x)
            });
            return tcs.Task;
        }


        public virtual Task<PromptResult> PromptAsync(PromptConfig config) {
            var tcs = new TaskCompletionSource<PromptResult>();
            config.OnResult = x => tcs.TrySetResult(x);
            this.Prompt(config);
            return tcs.Task;
        }
    }
}
