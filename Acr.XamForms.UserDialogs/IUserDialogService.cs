using System;
using System.Threading.Tasks;


namespace Acr.XamForms.UserDialogs {

    public interface IUserDialogService {

        void Alert(string message, string title = null, string okText = "OK", Action onOk = null);
        Task AlertAsync(string message, string title = null, string okText = "OK");
        
        void ActionSheet(string title = null, params ActionSheetOption[] options);
        void ActionSheet(Action<ActionSheetOptions> config);
        void ActionSheet(ActionSheetOptions options);

        void Confirm(string message, Action<bool> onConfirm, string title = null, string okText = "OK", string cancelText = "Cancel");
        Task<bool> ConfirmAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel");

        void Prompt(string message, Action<PromptResult> promptResult, string title = null, string okText = "OK", string cancelText = "Cancel", string hint = null);
        Task<PromptResult> PromptAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel", string hint = null);
        
        IProgressDialog Progress(string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true);
        IProgressDialog Loading(string title = "Loading", Action onCancel = null, string cancelText = "Cancel", bool show = true);

        void ShowLoading(string title = "Loading");
        void HideLoading();

        void Toast(string message, int timeoutSeconds = 3, Action onClick = null);
    }
}