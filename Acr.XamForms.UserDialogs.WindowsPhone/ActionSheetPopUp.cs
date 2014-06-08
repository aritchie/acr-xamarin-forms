using System;
using System.Windows;
using System.Windows.Controls;
using Coding4Fun.Toolkit.Controls;


namespace Acr.XamForms.UserDialogs.WindowsPhone {
    
    public class ActionSheetPopUp : MessagePrompt {

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            this.ActionButtonArea.Children.Clear();
            
            var stackPanel = new StackPanel {
                Orientation = Orientation.Vertical,
                Margin = new Thickness()
            };
            this.ActionPopUpButtons.ForEach(stackPanel.Children.Add);
            this.ActionButtonArea.Children.Add(stackPanel);
        }


        public void AddButton(string text, Action action) {
            var btn = new Button {
                Content = text,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
            btn.Click += (sender, args) => {
                this.Hide();
                if (action != null)
                    action();
            };
            this.ActionPopUpButtons.Add(btn);
        }
    }
}
