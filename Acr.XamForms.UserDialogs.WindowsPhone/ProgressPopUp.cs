using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;


namespace Acr.XamForms.UserDialogs.WindowsPhone {
    
    public class ProgressPopUp : CustomMessageBox {

        private readonly Button cancelButton = new Button();
        private readonly TextBlock percentText = new TextBlock {
            Visibility = Visibility.Collapsed
        };
        private readonly ProgressBar progressBar = new ProgressBar {
            HorizontalAlignment = HorizontalAlignment.Stretch
        };


        public ProgressPopUp() {
            this.IsRightButtonEnabled = false;
            this.IsLeftButtonEnabled = false;
            this.RightButtonContent = this.cancelButton;
            
            var stack = new StackPanel {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            stack.Children.Add(this.progressBar);
            stack.Children.Add(this.percentText);
            this.Content = stack;
        }


        public bool IsIndeterminate {
            get { return this.progressBar.IsIndeterminate; }
            set {
                this.progressBar.IsIndeterminate = value;
                this.percentText.Visibility = (value 
                    ? Visibility.Collapsed
                    : Visibility.Visible
                );
            }
        }


        public string LoadingText {
            get { return this.Title; }
            set { this.Title = value; }
        }


        public string CompletionText {
            get { return this.percentText.Text; }
            set { this.percentText.Text = value; }
        }


        public int PercentComplete {
            get { return Convert.ToInt32(this.progressBar.Value); }
            set { this.progressBar.Value = value; }
        }


        public void SetCancel(Action action, string cancelText) {
            this.cancelButton.Click += (sender, args) => this.Dismiss();
            this.Dismissed += (sender, args) => action();
            this.cancelButton.Content = cancelText;
            this.IsRightButtonEnabled = true;
        }
    }
}
