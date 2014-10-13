using System;
using System.Drawing;
using System.Linq;
using Acr.XamForms.UserDialogs.iOS;
using BigTed;
using MonoTouch.UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserDialogService))]


namespace Acr.XamForms.UserDialogs.iOS {

    public class UserDialogService : AbstractUserDialogService {

        public override void ActionSheet(ActionSheetConfig config) {
            Device.BeginInvokeOnMainThread(() => {

                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                    var sheet = UIAlertController.Create(config.Title ?? String.Empty, String.Empty, UIAlertControllerStyle.ActionSheet);
                    config.Options.ToList().ForEach(x => 
                        sheet.AddAction(UIAlertAction.Create(x.Text, UIAlertActionStyle.Default, y => {
                            if (x.Action != null)
                                x.Action();
                        }))
                    );
                    Present(sheet);
                }
                else {
                    var view = Utils.GetTopView();

                    var action = new UIActionSheet(config.Title);
                    config.Options.ToList().ForEach(x => action.AddButton(x.Text));

                    action.Dismissed += (sender, btn) => config.Options[btn.ButtonIndex].Action();
                    action.ShowInView(view);
                }
            });
        }


        public override void Alert(AlertConfig config) {
            Device.BeginInvokeOnMainThread(() => {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                    var alert = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, x => {
                        if (config.OnOk != null)
                            config.OnOk();
                    }));
                    Present(alert);
                }
                else {
                    var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, null, config.OkText);
                    if (config.OnOk != null) 
                        dlg.Clicked += (s, e) => config.OnOk();

                    dlg.Show();
                }
            });
        }


        public override void Confirm(ConfirmConfig config) {
            Device.BeginInvokeOnMainThread(() => {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                    var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnConfirm(true)));
                    dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => config.OnConfirm(false)));
                    Present(dlg);
                }
                else {
                    var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, config.CancelText, config.OkText);
                    dlg.Clicked += (s, e) => {
                        var ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                        config.OnConfirm(ok);
                    };
                    dlg.Show();
                }
            });
        }


        public override void Login(LoginConfig config) {
            UITextField txtUser = null;
            UITextField txtPass = null;

            Device.BeginInvokeOnMainThread(() => {

                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                    var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, true))));
                    dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, true))));

                    dlg.AddTextField(x => {
                        txtUser = x;
                        x.Placeholder = config.LoginPlaceholder;
                        x.Text = config.LoginValue ?? String.Empty;
                    });
                    dlg.AddTextField(x => {
                        txtPass = x;
                        x.Placeholder = config.PasswordPlaceholder;
                        x.SecureTextEntry = true;
                    });
                    Present(dlg);
                }
                else {
                    var dlg = new UIAlertView { AlertViewStyle = UIAlertViewStyle.LoginAndPasswordInput };
                    txtUser = dlg.GetTextField(0);
                    txtPass = dlg.GetTextField(1);

                    txtUser.Placeholder = config.LoginPlaceholder;
                    txtUser.Text = config.LoginValue ?? String.Empty;
                    txtPass.Placeholder = config.PasswordPlaceholder;

                    dlg.Clicked += (s, e) => {
                        var ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                        config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, ok));
                    };
                    dlg.Show();
                }
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            // TODO: no click callback in showtoast at the moment
            Device.BeginInvokeOnMainThread(() => {
                var ms = timeoutSeconds * 1000;
                BTProgressHUD.ShowToast(message, false, ms);
            });
        }


        public override void Prompt(PromptConfig config) {
            Device.BeginInvokeOnMainThread(() => {
                var result = new PromptResult();

                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                    var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    UITextField txt = null;

                    dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => {
                        result.Ok = true;
                        result.Text = txt.Text.Trim();
                        config.OnResult(result);
                    }));
                    dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => {
                        result.Ok = false;
                        result.Text = txt.Text.Trim();
                        config.OnResult(result);
                    }));
                    dlg.AddTextField(x => {
                        x.SecureTextEntry = config.IsSecure;
                        x.Placeholder = config.Placeholder ?? String.Empty;
                        txt = x;
                    });
                    Present(dlg);
                }
                else {
                    var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, config.CancelText, config.OkText) {
                        AlertViewStyle = config.IsSecure
                            ? UIAlertViewStyle.SecureTextInput 
                            : UIAlertViewStyle.PlainTextInput
                    };
                    var txt = dlg.GetTextField(0);
                    txt.SecureTextEntry = config.IsSecure;
                    txt.Placeholder = config.Placeholder;

                    dlg.Clicked += (s, e) => {
                        result.Ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                        result.Text = txt.Text.Trim();
                        config.OnResult(result);
                    };
                    dlg.Show();
                }
            });
        }


        protected override IProgressDialog CreateDialogInstance() {
            return new ProgressDialog();
        }


        private static void Present(UIAlertController controller) {
            Device.BeginInvokeOnMainThread(() => {
                             var top = Utils.GetTopViewController();
                var po = controller.PopoverPresentationController;
                if (po != null) {
                    po.SourceView = top.View;
                    var viewHeight = top.View.Frame.Height;
                    var viewWidth = top.View.Frame.Width;
                    var sheetHeight = 400;
                    var sheetWidth = 300;
                    var h = (viewHeight / 2) - (sheetHeight / 2);
                    var v = (viewWidth / 2) - (sheetWidth / 2);
                    po.SourceRect = new RectangleF(v, h, sheetWidth, sheetHeight);
                    po.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                }
                top.PresentViewController(controller, true, null);
            });
        }
        //public override void DateTimePrompt(DateTimePromptConfig config) {
        //    var sheet = new ActionSheetDatePicker {
        //        Title = config.Title,
        //        DoneText = config.OkText
        //    };

        //    switch (config.SelectionType) {
        //        case DateTimeSelectionType.Date:
        //            sheet.DatePicker.Mode = UIDatePickerMode.Date;
        //            break;

        //        case DateTimeSelectionType.Time:
        //            sheet.DatePicker.Mode = UIDatePickerMode.Time;
        //            break;

        //        case DateTimeSelectionType.DateTime:
        //            sheet.DatePicker.Mode = UIDatePickerMode.DateAndTime;
        //            break;
        //    }
        //    if (config.MinValue != null)
        //        sheet.DatePicker.MinimumDate = config.MinValue.Value;

        //    if (config.MaxValue != null)
        //        sheet.DatePicker.MaximumDate = config.MaxValue.Value;

        //    sheet.DateTimeSelected += (sender, args) => {
        //        // TODO: stop adjusting date/time
        //        config.OnResult(new DateTimePromptResult(sheet.DatePicker.Date));
        //    };

        //    var top = Utils.GetTopView();
        //    sheet.Show(top);
        //    //sheet.DatePicker.MinuteInterval
        //}


        //public override void DurationPrompt(DurationPromptConfig config) {
        //    var sheet = new ActionSheetDatePicker {
        //        Title = config.Title,
        //        DoneText = config.OkText
        //    };
        //    sheet.DatePicker.Mode = UIDatePickerMode.CountDownTimer;

        //    sheet.DateTimeSelected += (sender, args) => config.OnResult(new DurationPromptResult(args.TimeOfDay));

        //    var top = Utils.GetTopView();
        //    sheet.Show(top);
        //}
    }
}