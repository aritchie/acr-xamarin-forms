using System;
using CoreGraphics;
using System.Linq;
using Acr.XamForms.UserDialogs.iOS;
using BigTed;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserDialogService))]


namespace Acr.XamForms.UserDialogs.iOS
{

    public class UserDialogService : AbstractUserDialogService
    {

        public override void ActionSheet(ActionSheetConfig config)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var sheet = UIAlertController.Create(config.Title ?? String.Empty, String.Empty, UIAlertControllerStyle.ActionSheet);
                    config.Options.ToList().ForEach(x =>
                        sheet.AddAction(UIAlertAction.Create(x.Text, UIAlertActionStyle.Default, y =>
                        {
                            if (x.Action != null)
                                x.Action();
                        }))
                    );
                    Present(sheet);
                }
                else
                {
                    var view = Utils.GetTopView();

                    var action = new UIActionSheet(config.Title);
                    config.Options.ToList().ForEach(x => action.AddButton(x.Text));

                    action.Dismissed += (sender, btn) =>
                    {
                        if ((int)btn.ButtonIndex > -1 && (int)btn.ButtonIndex < config.Options.Count)
                            config.Options[(int)btn.ButtonIndex].Action();
                    };
                    action.ShowInView(view);
                }
            });
        }


        public override void Alert(AlertConfig config)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var alert = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, x =>
                    {
                        if (config.OnOk != null)
                            config.OnOk();
                    }));
                    Present(alert);
                }
                else
                {
                    var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, null, config.OkText);
                    if (config.OnOk != null)
                        dlg.Clicked += (s, e) => config.OnOk();

                    dlg.Show();
                }
            });
        }


        public override void Confirm(ConfirmConfig config)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnConfirm(true)));
                    dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => config.OnConfirm(false)));
                    Present(dlg);
                }
                else
                {
                    var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, config.CancelText, config.OkText);
                    dlg.Clicked += (s, e) =>
                    {
                        var ok = ((int)dlg.CancelButtonIndex != (int)e.ButtonIndex);
                        config.OnConfirm(ok);
                    };
                    dlg.Show();
                }
            });
        }


        public override void Login(LoginConfig config)
        {
            UITextField txtUser = null;
            UITextField txtPass = null;

            Device.BeginInvokeOnMainThread(() =>
            {

                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, true))));
                    dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, false))));

                    dlg.AddTextField(x =>
                    {
                        txtUser = x;
                        x.Placeholder = config.LoginPlaceholder;
                        x.Text = config.LoginValue ?? String.Empty;
                    });
                    dlg.AddTextField(x =>
                    {
                        txtPass = x;
                        x.Placeholder = config.PasswordPlaceholder;
                        x.SecureTextEntry = true;
                    });
                    Present(dlg);
                }
                else
                {
                    var dlg = new UIAlertView { AlertViewStyle = UIAlertViewStyle.LoginAndPasswordInput };
                    txtUser = dlg.GetTextField((nint)0);
                    txtPass = dlg.GetTextField((nint)1);

                    txtUser.Placeholder = config.LoginPlaceholder;
                    txtUser.Text = config.LoginValue ?? String.Empty;
                    txtPass.Placeholder = config.PasswordPlaceholder;

                    dlg.Clicked += (s, e) =>
                    {
                        var ok = ((int)dlg.CancelButtonIndex != (int)e.ButtonIndex);
                        config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, ok));
                    };
                    dlg.Show();
                }
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick)
        {
            // TODO: no click callback in showtoast at the moment
            Device.BeginInvokeOnMainThread(() =>
            {
                var ms = timeoutSeconds * 1000;
                BTProgressHUD.ShowToast(message, false, ms);
            });
        }

        public override void ShowNetworkLoading(string message = "Downloading")
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
        }

        public override void HideNetworkLoading()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
        }

        public override IDownloadDialog NetworkLoading(string message = "Downloading")
        {
            return new DownloadDialog();
        }

        public override void Prompt(PromptConfig config)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var result = new PromptResult();

                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
                    UITextField txt = null;

                    dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x =>
                    {
                        result.Ok = true;
                        result.Text = txt.Text.Trim();
                        config.OnResult(result);
                    }));
                    dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x =>
                    {
                        result.Ok = false;
                        result.Text = txt.Text.Trim();
                        config.OnResult(result);
                    }));
                    dlg.AddTextField(x =>
                    {
                        x.SecureTextEntry = config.IsSecure;
                        x.Placeholder = config.Placeholder ?? String.Empty;
                        txt = x;
                    });
                    Present(dlg);
                }
                else
                {
                    var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, config.CancelText, config.OkText)
                    {
                        AlertViewStyle = config.IsSecure
                            ? UIAlertViewStyle.SecureTextInput
                            : UIAlertViewStyle.PlainTextInput
                    };
                    var txt = dlg.GetTextField((nint)0);
                    txt.SecureTextEntry = config.IsSecure;
                    txt.Placeholder = config.Placeholder;

                    dlg.Clicked += (s, e) =>
                    {
                        result.Ok = ((int)dlg.CancelButtonIndex != (int)e.ButtonIndex);
                        result.Text = txt.Text.Trim();
                        config.OnResult(result);
                    };
                    dlg.Show();
                }
            });
        }


        protected override IProgressDialog CreateDialogInstance()
        {
            return new ProgressDialog();
        }


        private static void Present(UIAlertController controller)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var top = Utils.GetTopViewController();
                var po = controller.PopoverPresentationController;
                if (po != null)
                {
                    po.SourceView = top.View;
                    var h = (top.View.Frame.Height / 2) - 400;
                    var v = (top.View.Frame.Width / 2) - 300;
                    po.SourceRect = new CGRect(v, h, 0, 0);
                    po.PermittedArrowDirections = UIPopoverArrowDirection.Any;
                }
                top.PresentViewController(controller, true, null);
            });
        }
    }
}