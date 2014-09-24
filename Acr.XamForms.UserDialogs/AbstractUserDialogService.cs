using System;


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
        protected abstract IProgressDialog CreateDialogInstance();


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
    }
}
