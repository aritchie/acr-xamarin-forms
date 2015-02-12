using System;
using System.Threading.Tasks;


namespace Acr.XamForms.UserDialogs {

    public interface IUserDialogService {

        void Alert(string message, string title = null, string okText = "OK");
        void Alert(AlertConfig config);
        void ActionSheet(ActionSheetConfig config);
        void Confirm(ConfirmConfig config);
        void Prompt(PromptConfig config);
        void Login(LoginConfig config);
        //void DateTimePrompt(DateTimePromptConfig config);
        //void DurationPrompt(DurationPromptConfig config);
        IProgressDialog Progress(ProgressConfig config);
        IProgressDialog Loading(string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true);
        IProgressDialog Progress(string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true);

        void ShowLoading(string title = "Loading");
        void HideLoading();
        void Toast(string message, int timeoutSeconds = 3, Action onClick = null);

		void ShowSuccess(string message, int timeoutSeconds = 3, Action onClick = null);
		void ShowError(string message, int timeoutSeconds = 3, Action onClick = null);

        Task AlertAsync(string message, string title = null, string okText = "OK");
        Task AlertAsync(AlertConfig config);
        Task<bool> ConfirmAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel");
        Task<bool> ConfirmAsync(ConfirmConfig config);
        Task<LoginResult> LoginAsync(string title = "Login", string message = null);
        Task<LoginResult> LoginAsync(LoginConfig config);
        Task<PromptResult> PromptAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel", string placeholder = "", bool secure = false);
        Task<PromptResult> PromptAsync(PromptConfig config);
    }
}