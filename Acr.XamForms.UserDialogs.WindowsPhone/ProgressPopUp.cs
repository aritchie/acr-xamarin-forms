using System;
using System.Windows;
using System.Windows.Controls;
using Coding4Fun.Toolkit.Controls;


namespace Acr.XamForms.UserDialogs.WindowsPhone {
    
    public class ProgressPopUp : MessagePrompt {

        private readonly TextBlock percentText = new TextBlock {
            HorizontalAlignment = HorizontalAlignment.Center
        };
        private readonly Button cancelButton = new Button {
            Visibility = Visibility.Collapsed,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        private readonly ProgressBar progressBar = new ProgressBar {
            HorizontalAlignment = HorizontalAlignment.Stretch
        };


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
            this.cancelButton.Click += (sender, args) => action();
            this.cancelButton.Content = cancelText;
            this.cancelButton.Visibility = Visibility.Visible;
        }


        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            this.ActionButtonArea.Children.Clear();
            
            var stackPanel = new StackPanel {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            stackPanel.Children.Add(this.progressBar);
            stackPanel.Children.Add(this.percentText);
            stackPanel.Children.Add(this.cancelButton);

            this.ActionButtonArea.Children.Add(stackPanel);
        }
    }
}
