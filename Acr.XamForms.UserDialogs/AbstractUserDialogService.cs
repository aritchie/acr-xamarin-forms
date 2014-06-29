using System;


namespace Acr.XamForms.UserDialogs {

    public abstract class AbstractUserDialogService : IUserDialogService {
        
        public abstract void Alert(AlertConfig config);
        public abstract void ActionSheet(ActionSheetConfig config);
        public abstract void Confirm(ConfirmConfig config);
        public abstract void Prompt(PromptConfig config);
        public abstract void Toast(string message, int timeoutSeconds = 3, Action onClick = null);
        protected abstract IProgressDialog CreateDialogInstance();


        private IProgressDialog loading;
        public virtual void ShowLoading(string title) {
            //if (this.loading == null) 
            //    this.Loading(title, null, null, true);
        }


        public virtual void HideLoading() {
            if (this.loading != null) {
                this.loading.Dispose();
                this.loading = null;
            }
        }


        public virtual IProgressDialog Progress(ProgressConfig config) {
            var dlg = this.CreateDialogInstance();
            //dlg.Title = title;
            //dlg.IsDeterministic = isdeterministic;

            //if (onCancel != null) 
            //    dlg.SetCancel(onCancel, cancelText);
            
            //if (show) 
            //    dlg.Show();
            
            return dlg;
        }
    }
}
