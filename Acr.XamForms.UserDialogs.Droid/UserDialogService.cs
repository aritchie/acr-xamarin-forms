using System;
using System.Linq;
using Acr.XamForms.UserDialogs.Droid;
using Android.App;
using Android.Content;
using Android.Text.Method;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidHUD;
using Xamarin.Forms;
using Android.Text;

[assembly: Dependency(typeof(UserDialogService))]


namespace Acr.XamForms.UserDialogs.Droid {
    
    public class UserDialogService : AbstractUserDialogService {

        public override void Alert(AlertConfig config) {
            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
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
                    .Builder(Utils.GetActivityContext())
                    .SetTitle(config.Title)
                    .SetItems(array, (sender, args) => config.Options[args.Which].Action())
                    .Show()
            );
        }


        public override void Confirm(ConfirmConfig config) {
            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) => config.OnConfirm(true))
                    .SetNegativeButton(config.CancelText, (o, e) => config.OnConfirm(false))
                    .Show()
            );
        }


        public override void Login(LoginConfig config) {
            var context = Utils.GetActivityContext();
            var txtUser = new EditText(context) {
                Hint = config.LoginPlaceholder,
                Text = config.LoginValue ?? String.Empty,
				InputType = InputTypes.TextVariationVisiblePassword
            };
			txtUser.SetMaxLines(1);

            var txtPass = new EditText(context) {
                Hint = config.PasswordPlaceholder ?? "*",
                TransformationMethod = PasswordTransformationMethod.Instance,
				InputType = InputTypes.ClassText | InputTypes.TextVariationPassword
            };
			txtPass.SetMaxLines(1);

            var layout = new LinearLayout(context) {
                Orientation = Orientation.Vertical
            };
            layout.AddView(txtUser, ViewGroup.LayoutParams.MatchParent);
            layout.AddView(txtPass, ViewGroup.LayoutParams.MatchParent);

            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetTitle(config.Title)
                    .SetMessage(config.Message)
                    .SetView(layout)
                    .SetPositiveButton(config.OkText, (o, e) =>
                        config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, true))
                    )
                    .SetNegativeButton(config.CancelText, (o, e) =>
                        config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, false))
                    )
                    .Show()
            );
        }


        public override void Prompt(PromptConfig config) {
            Utils.RequestMainThread(() => {
                var txt = new EditText(Utils.GetActivityContext()) {
                    Hint = config.Placeholder
                };
                txt.SetMaxLines(1);
				if (config.IsSecure) {
                    txt.TransformationMethod = PasswordTransformationMethod.Instance;
					txt.InputType = InputTypes.ClassText | InputTypes.TextVariationPassword;
				}
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
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
                    Utils.GetActivityContext(), 
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