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

        public override void ActionSheet(ActionSheetConfig config) {
            this.Dispatch(() => {
                var alert = new ActionSheetPopUp { Title = config.Title };
                alert.ActionPopUpButtons.Clear();
                config.Options.ToList().ForEach(x => alert.AddButton(x.Text, x.Action));
                alert.Show();
            });
        }


        public override void Alert(AlertConfig config) {
            this.Dispatch(() => {
                var alert = new MessagePrompt {
                    Title = config.Title,
                    Message = config.Message
                };
                var btn = new Button { Content = config.OkText };
                btn.Click += (sender, args) => alert.Hide();

                if (config.OnOk != null)  
                    alert.Completed += (sender, args) => config.OnOk();
                
                alert.ActionPopUpButtons.Clear();
                alert.ActionPopUpButtons.Add(btn);
                alert.Show();
            });
        }


        public override void Confirm(ConfirmConfig config) {
            this.Dispatch(() => {
                var alert = new MessagePrompt {
                    Title = config.Title,
                    Message = config.Message
                };
                var btnYes = new Button { Content = config.OkText };
                btnYes.Click += (sender, args) => {
                    alert.Hide();
                    config.OnConfirm(true);
                };

                var btnNo = new Button { Content = config.CancelText };
                btnNo.Click += (sender, args) => {
                    alert.Hide();
                    config.OnConfirm(false);
                };

                alert.ActionPopUpButtons.Clear();
                alert.ActionPopUpButtons.Add(btnYes);
                alert.ActionPopUpButtons.Add(btnNo);
                alert.Show();
            });
        }


        //public override void PickDate(Action<DatePickerResult> callback, string title, DateTime? selectedDateTime, DateTime? minDate, DateTime? maxDate) {
        //    var picker = new DatePickerPage();
        //}


        public override void Prompt(PromptConfig config) {
            this.Dispatch(() => {
                var yes = false;

                // TODO: secure input - passwordbox
                var input = new InputPrompt {
                    Title = config.Title,
                    Message = config.Message,
                    IsCancelVisible = true
                };

                input.ActionPopUpButtons.Clear();

                var btnYes = new Button { Content = config.OkText };
                btnYes.Click += (sender, args) => {
                    yes = true;
                    input.Hide();
                };

                var btnNo = new Button { Content = config.CancelText };
                btnNo.Click += (sender, args) => input.Hide();

                input.ActionPopUpButtons.Clear();
                input.ActionPopUpButtons.Add(btnYes);
                input.ActionPopUpButtons.Add(btnNo);
            
                input.Completed += (sender, args) => config.OnResult(new PromptResult {
                    Ok = yes,
                    Text = input.Value
                });
                input.Show();
            });
        }


        public override void DateTimePrompt(DateTimePromptConfig config) {
            // TODO
            throw new NotImplementedException();
        }


        public override void DurationPrompt(DurationPromptConfig config) {
            // TODO
            throw new NotImplementedException();
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
