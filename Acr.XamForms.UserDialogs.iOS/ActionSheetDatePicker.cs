// CREDIT: http://developer.xamarin.com/recipes/ios/standard_controls/actionsheet/actionsheet_date_picker/
using System;
using System.Drawing;
using MonoTouch.UIKit;


namespace Acr.XamForms.UserDialogs.iOS {
	
    public class ActionSheetDatePicker {
        private const int TitleBarHeight = 40;

        private readonly UIActionSheet actionSheet;
        private readonly UIButton doneButton = UIButton.FromType(UIButtonType.RoundedRect);
        //private readonly UIButton cancelButton = UIButton.FromType(UIButtonType.RoundedRect);
        private readonly UIView owner;
        private readonly UILabel titleLabel = new UILabel();
	

        public ActionSheetDatePicker(UIView owner) {
            this.owner = owner;
            this.DatePicker = new UIDatePicker(RectangleF.Empty);
	
            this.titleLabel.BackgroundColor = UIColor.Clear;
            this.titleLabel.TextColor = UIColor.LightTextColor;
            this.titleLabel.Font = UIFont.BoldSystemFontOfSize (18);
		
            this.doneButton.TouchUpInside += (s, e) => {
                actionSheet.DismissWithClickedButtonIndex(0, true);
                if (this.DateTimeSelected != null)
                    this.DateTimeSelected(this, this.DatePicker.Date);
            };
			
            this.actionSheet = new UIActionSheet { Style = UIActionSheetStyle.BlackTranslucent };
	
            // TODO: cancel
            this.actionSheet.AddSubview(this.DatePicker);
            this.actionSheet.AddSubview(this.titleLabel);
            this.actionSheet.AddSubview(this.doneButton);
        }

        #region Properties

        public event EventHandler<DateTime> DateTimeSelected;
        public UIDatePicker DatePicker { get; private set; }
		
        public string Title {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }


        public string DoneText {
            get { return this.doneButton.Title(UIControlState.Normal); }
            set { this.doneButton.SetTitle(value, UIControlState.Normal); }
        }


        //public string CancelText {
        //    get { }
        //}
        #endregion

        #region Methods

        public void Show() {
            var doneButtonSize = new SizeF(71, 30);
            var actionSheetSize = new SizeF(owner.Frame.Width, this.DatePicker.Frame.Height + TitleBarHeight);
            var actionSheetFrame = new RectangleF(
                0, 
                owner.Frame.Height - actionSheetSize.Height, 
                actionSheetSize.Width, 
                actionSheetSize.Height
            );
			
            this.actionSheet.ShowInView(this.owner);
			
            // resize the action sheet to fit our other stuff
            this.actionSheet.Frame = actionSheetFrame;
			
            // move our picker to be at the bottom of the actionsheet (view coords are relative to the action sheet)
            this.DatePicker.Frame = new RectangleF(
                this.DatePicker.Frame.X, 
                TitleBarHeight, 
                this.DatePicker.Frame.Width, 
                this.DatePicker.Frame.Height
            );
			
            // move our label to the top of the action sheet
            this.titleLabel.Frame = new RectangleF(10, 4, this.owner.Frame.Width - 100, 35);
			
            // move our button
            this.doneButton.Frame = new RectangleF(actionSheetSize.Width - doneButtonSize.Width - 10, 7, doneButtonSize.Width, doneButtonSize.Height);
        }
		

        public void Hide(bool animated) {
            actionSheet.DismissWithClickedButtonIndex(0, animated);
        }
		
        #endregion		
    }
}

