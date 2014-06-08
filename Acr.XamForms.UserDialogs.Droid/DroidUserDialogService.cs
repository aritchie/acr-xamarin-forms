using System;
using System.Linq;
using Acr.XamForms.UserDialogs.Droid;
using Android.App;
using Android.Widget;
using AndroidHUD;
using Xamarin.Forms;


[assembly: Dependency(typeof(DroidUserDialogService))]


namespace Acr.XamForms.UserDialogs.Droid {
    
    public class DroidUserDialogService : AbstractUserDialogService<DroidProgressDialog> {

        public override void Alert(string message, string title, string okText, Action onOk) {
            Droid.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetPositiveButton(okText, (o, e) => {
                        if (onOk != null) 
                            onOk();
                    })
                    .Show()
            );
        }


        public override void ActionSheet(ActionSheetOptions options) {
            var array = options
                .Options
                .Select(x => x.Text)
                .ToArray();

            Droid.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetTitle(options.Title)
                    .SetItems(array, (sender, args) => options.Options[args.Which].Action())
                    .Show()
            );
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            Droid.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(message)
                        .SetTitle(title)
                        .SetPositiveButton(okText, (o, e) => onConfirm(true))
                        .SetNegativeButton(cancelText, (o, e) => onConfirm(false))
                        .Show()
            );
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            Droid.RequestMainThread(() => {
                var txt = new EditText(Forms.Context) {
                    Hint = hint
                };

                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetView(txt)
                    .SetPositiveButton(okText, (o, e) =>
                        promptResult(new PromptResult {
                            Ok = true, 
                            Text = txt.Text
                        })
                    )
                    .SetNegativeButton(cancelText, (o, e) => 
                        promptResult(new PromptResult {
                            Ok = false, 
                            Text = txt.Text
                        })
                    )
                    .Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            Droid.RequestMainThread(() => {
                onClick = onClick ?? (() => {});

                AndHUD.Shared.ShowToast(
                    Forms.Context, 
                    message, 
                    MaskType.Clear,
                    TimeSpan.FromSeconds(timeoutSeconds),
                    false,
                    onClick
                );
            });
        }


        protected override DroidProgressDialog CreateProgressDialogInstance() {
            return new DroidProgressDialog();
        }
    }
}