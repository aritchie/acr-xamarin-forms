using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Acr.XamForms.UserDialogs.WindowsPhone;
using Coding4Fun.Toolkit.Controls;
using Xamarin.Forms;
using Button = System.Windows.Controls.Button;

[assembly: Dependency(typeof(UserDialogService))]


namespace Acr.XamForms.UserDialogs.WindowsPhone {
    
    public class UserDialogService : AbstractUserDialogService {

        public override void ActionSheet(ActionSheetOptions options) {
            this.Dispatch(() => {
                var alert = new ActionSheetPopUp { Title = options.Title };
                alert.ActionPopUpButtons.Clear();
                options.Options.ToList().ForEach(x => alert.AddButton(x.Text, x.Action));
                alert.Show();
            });
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            this.Dispatch(() => {
                var alert = new MessagePrompt {
                    Title = title,
                    Message = message
                };
                var btn = new Button { Content = okText };
                btn.Click += (sender, args) => alert.Hide();

                if (onOk != null) { 
                    alert.Completed += (sender, args) => onOk();
                }
                alert.ActionPopUpButtons.Clear();
                alert.ActionPopUpButtons.Add(btn);
                alert.Show();
            });
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            this.Dispatch(() => {
                var alert = new MessagePrompt {
                    Title = title,
                    Message = message
                };
                var btnYes = new Button { Content = okText };
                btnYes.Click += (sender, args) => {
                    alert.Hide();
                    onConfirm(true);
                };

                var btnNo = new Button { Content = cancelText };
                btnNo.Click += (sender, args) => {
                    alert.Hide();
                    onConfirm(false);
                };

                alert.ActionPopUpButtons.Clear();
                alert.ActionPopUpButtons.Add(btnYes);
                alert.ActionPopUpButtons.Add(btnNo);
                alert.Show();
            });
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string placeholder, int textLines) {
            // TODO: multiline text
            this.Dispatch(() => {
                var yes = false;

                var input = new InputPrompt {
                    Title = title,
                    Message = message,
                    IsCancelVisible = true,
                };
                input.ActionPopUpButtons.Clear();

                var btnYes = new Button { Content = okText };
                btnYes.Click += (sender, args) => {
                    yes = true;
                    input.Hide();
                };

                var btnNo = new Button { Content = cancelText };
                btnNo.Click += (sender, args) => input.Hide();

                input.ActionPopUpButtons.Clear();
                input.ActionPopUpButtons.Add(btnYes);
                input.ActionPopUpButtons.Add(btnNo);
            
                input.Completed += (sender, args) => promptResult(new PromptResult {
                    Ok = yes,
                    Text = input.Value
                });
                input.Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            this.Dispatch(() => {
                var toast = new ToastPrompt {
                    Message = message,
                    MillisecondsUntilHidden = timeoutSeconds * 1000
                };
                if (onClick != null) {
                    toast.Tap += (sender, args) => onClick();
                }
                toast.Show();
            });
        }


        protected virtual void Dispatch(Action action) {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }


        protected override IProgressDialog CreateDialogInstance() {
            return new ProgressDialog();
        }


        protected virtual Popup CreatePopup(UserControl control) {
            var size = Application.Current.RootVisual.RenderSize;

            return new Popup {
                VerticalOffset = (size.Width - control.ActualWidth) / 2,
                HorizontalOffset = (size.Height - control.ActualHeight) / 2,
                Width = size.Width,
                Height = size.Height,
                Child = control
            };
        }
    }
}
