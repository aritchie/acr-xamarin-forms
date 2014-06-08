using System;
using System.Linq;
using Acr.XamForms.UserDialogs.iOS;
using BigTed;
using MonoTouch.UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSUserDialogService))]


namespace Acr.XamForms.UserDialogs.iOS {
    
    public class iOSUserDialogService : AbstractUserDialogService<iOSProgressDialog> {

        public override void ActionSheet(ActionSheetOptions options) {
            this.Dispatch(() => {
                var action = new UIActionSheet(options.Title);
                options.Options.ToList().ForEach(x => action.AddButton(x.Text));

                action.Clicked += (sender, btn) => options.Options[btn.ButtonIndex].Action();
                var view = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
                action.ShowInView(view);
            });
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            this.Dispatch(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, null, okText);
                if (onOk != null) 
                    dlg.Clicked += (s, e) => onOk();
                
                dlg.Show();
            });
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            this.Dispatch(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, cancelText, okText);
                dlg.Clicked += (s, e) => {
                    var ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    onConfirm(ok);
                };
                dlg.Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            // TODO: no click callback in showtoast at the moment
            this.Dispatch(() => {
                var ms = timeoutSeconds * 1000;
                BTProgressHUD.ShowToast(message, false, ms);
            });
            
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            this.Dispatch(() => {
                var result = new PromptResult();
                var dlg = new UIAlertView(title ?? String.Empty, message, null, cancelText, okText) {
                    AlertViewStyle = UIAlertViewStyle.PlainTextInput
                };
                var txt = dlg.GetTextField(0);
                txt.Placeholder = hint;

                dlg.Clicked += (s, e) => {
                    result.Ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    result.Text = txt.Text;
                    promptResult(result);
                };
                dlg.Show();
            });
        }


        protected override iOSProgressDialog CreateProgressDialogInstance() {
            return new iOSProgressDialog();
        }


        protected virtual void Dispatch(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}