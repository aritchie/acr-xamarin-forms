using System;
using System.Linq;
using System.Threading;
using Acr.XamForms.UserDialogs.Droid;
using Android.App;
using Android.Text;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidHUD;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserDialogService))]


namespace Acr.XamForms.UserDialogs.Droid {
    
    public class UserDialogService : AbstractUserDialogService {

        public override void Alert(AlertConfig config) {
            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) => {
                        if (config.OnOk != null) 
                            config.OnOk();
                    })
                    .Show()
            );
        }


        public override void ActionSheet(ActionSheetConfig config) {
            var array = config
                .Options
                .Select(x => x.Text)
                .ToArray();

            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetTitle(config.Title)
                    .SetItems(array, (sender, args) => config.Options[args.Which].Action())
                    .Show()
            );
        }


        public override void Confirm(ConfirmConfig config) {
            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) => config.OnConfirm(true))
                    .SetNegativeButton(config.CancelText, (o, e) => config.OnConfirm(false))
                    .Show()
            );
        }


        public override void DateTimePrompt(DateTimePromptConfig config) {
            var picker = new DatePickerDialog(Forms.Context, (sender, args) => {
                config.OnResult(new DateTimePromptResult(args.Date));
            }, 1900, 1, 1);
            
            picker.SetTitle(config.Title);
            picker.Show();
            //var time = new TimePickerDialog(Forms.Context, (sender, args) => {
                
            //}, 1, 1, false);

        //    //picker.SetButton(DialogButtonType.Negative, (sender, args) => {
        //    //    // TODO: cancel
        //    //});
        }


        public override void Prompt(PromptConfig config) {
            Utils.RequestMainThread(() => {
                var txt = new EditText(Forms.Context) {
                    Hint = config.Placeholder
                };
                switch (config.Type) {

                    case PromptType.Secure:
                        txt.InputType = InputTypes.TextVariationPassword;
                        break;

                    case PromptType.MultiLine:
                        txt.SetLines(3);
                        txt.SetSingleLine(false);
                        txt.ImeOptions = ImeAction.Next;
                        break;
                }

                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetView(txt)
                    .SetPositiveButton(config.OkText, (o, e) =>
                        config.OnResult(new PromptResult {
                            Ok = true, 
                            Text = txt.Text
                        })
                    )
                    .SetNegativeButton(config.CancelText, (o, e) => 
                        config.OnResult(new PromptResult {
                            Ok = false, 
                            Text = txt.Text
                        })
                    )
                    .Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            Utils.RequestMainThread(() => {
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


        protected override IProgressDialog CreateDialogInstance() {
            return new ProgressDialog();
        }
    }
}