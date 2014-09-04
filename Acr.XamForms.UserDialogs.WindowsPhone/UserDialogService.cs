using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Acr.XamForms.UserDialogs.WindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

[assembly: Xamarin.Forms.Dependency(typeof(UserDialogService))]


namespace Acr.XamForms.UserDialogs.WindowsPhone {
    
    public class UserDialogService : AbstractUserDialogService {

        public override void ActionSheet(ActionSheetConfig config) {
            var sheet = new CustomMessageBox {
                Caption = config.Title,
                IsLeftButtonEnabled = false,
                IsRightButtonEnabled = false
            };
            var list = new ListBox {
                FontSize = 36,
                Margin = new Thickness(12.0),
                SelectionMode = SelectionMode.Single,
                ItemsSource = config.Options
                    .Select(x => new TextBlock {
                        Text = x.Text,
                        Margin = new Thickness(0.0, 12.0, 0.0, 12.0),
                        DataContext = x
                    })
            };
            list.SelectionChanged += (sender, args) => sheet.Dismiss();
            sheet.Content = list;
            sheet.Dismissed += (sender, args) => {
                var txt = list.SelectedValue as TextBlock;
                if (txt == null)
                    return;

                var action = txt.DataContext as ActionSheetOption;
                if (action != null && action.Action != null)
                    action.Action();
            };
            this.Dispatch(sheet.Show);
        }


        public override void Alert(AlertConfig config) {
            this.Dispatch(() => {
                var alert = new CustomMessageBox {
                    Caption = config.Title,
                    Message = config.Message,
                    LeftButtonContent = config.OkText,
                    IsRightButtonEnabled = false
                };
                if (config.OnOk != null)
                    alert.Dismissed += (sender, args) => config.OnOk();

                alert.Show();
            });
        }
 

        public override void Confirm(ConfirmConfig config) {
            var confirm = new CustomMessageBox {
                Caption = config.Title,
                Message = config.Message,
                LeftButtonContent = config.OkText,
                RightButtonContent = config.CancelText
            };
            confirm.Dismissed += (sender, args) => config.OnConfirm(args.Result == CustomMessageBoxResult.LeftButton);
            this.Dispatch(confirm.Show);
        }


        public override void Prompt(PromptConfig config) {
            var prompt = new CustomMessageBox {
                Caption = config.Title,
                Message = config.Message,
                LeftButtonContent = config.OkText,
                RightButtonContent = config.CancelText
            };

            var password = new PasswordBox();
            var txt = new PhoneTextBox { Hint = config.Placeholder };
            if (config.IsSecure)
                prompt.Content = password;
            else 
                prompt.Content = txt;

            prompt.Dismissed += (sender, args) => config.OnResult(new PromptResult {
                Ok = args.Result == CustomMessageBoxResult.LeftButton,
                Text = config.IsSecure
                    ? password.Password
                    : txt.Text.Trim()
            });
            this.Dispatch(prompt.Show);
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            var resources = Application.Current.Resources;

            var tb = new TextBlock {
                Foreground = (Brush)resources["PhoneForegroundBrush"],
                FontSize = (double)resources["PhoneFontSizeMedium"],
                Margin = new Thickness(24, 32, 24, 12),
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = message
            };
            var wrapper = new StackPanel {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = (Brush)resources["PhoneAccentBrush"],
                Width = Application.Current.Host.Content.ActualWidth
            };
            wrapper.Children.Add(tb);

            var popup = new Popup {
                Child = wrapper,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            if (onClick != null) { 
                tb.Tap += (sender, args) => {
                    SystemTray.BackgroundColor = (Color)resources["PhoneBackgroundColor"];
                    popup.IsOpen = false;
                    onClick();
                };
            }

            this.Dispatch(() => {
                SystemTray.BackgroundColor = (Color)resources["PhoneAccentColor"];
                popup.IsOpen = true;
            });
            Task.Delay(TimeSpan.FromSeconds(timeoutSeconds))
                .ContinueWith(x => this.Dispatch(() => {
                    SystemTray.BackgroundColor = (Color)resources["PhoneBackgroundColor"];
                    popup.IsOpen = false;
                }));

        }


        protected virtual void Dispatch(Action action) {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }


        protected override IProgressDialog CreateDialogInstance() {
            return new ProgressDialog();
        }
    }
}
