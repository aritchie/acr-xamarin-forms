using System;
using System.Windows.Input;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class SignaturePadConfigViewModel : ViewModel {
        private readonly IUserDialogService dialogs;


        public SignaturePadConfigViewModel(IUserDialogService dialogs) {
            this.dialogs = dialogs;

            var cfg = SignaturePadConfiguration.Default;
            this.saveText = cfg.SaveText;
            this.cancelText = cfg.CancelText;
            this.promptText = cfg.PromptText;
            this.captionText = cfg.CaptionText;
        }


        private void SetTheme(string theme) {
            var c = SignaturePadConfiguration.Default;

            switch (theme.ToLower()) {
                case "black":
                    c.SignatureBackgroundColor = Color.Black;
                    c.ClearTextColor = Color.White;
                    c.PromptTextColor = Color.White;
                    c.SignatureLineColor = Color.White;
                    c.StrokeColor = Color.White;
                    break;

                case "white":
                    c.SignatureBackgroundColor = Color.White;
                    c.ClearTextColor = Color.Black;
                    c.PromptTextColor = Color.Black;
                    c.SignatureLineColor = Color.Black;
                    c.StrokeColor = Color.Black;
                    break;
            }
        }

        #region Binding Properties

        private ICommand changeThemeCmd;
        public ICommand ChangeTheme {
            get {
                this.changeThemeCmd = this.changeThemeCmd ?? new Command(() => this.dialogs.ActionSheet(new ActionSheetConfig()
                    .Add("Black", () => SetTheme("Black"))
                    .Add("White", () => SetTheme("White"))
                ));
                return this.changeThemeCmd;
            }
        }


        private string cancelText;
        public string CancelText {
            get { return this.cancelText; }
            set { 
                if (this.SetProperty(ref this.cancelText, value))
                    SignaturePadConfiguration.Default.CancelText = value;
            }
        }


        private string saveText;
        public string SaveText {
            get { return this.saveText; }
            set { 
                if (this.SetProperty(ref this.saveText, value))
                    SignaturePadConfiguration.Default.SaveText = value;
            }
        }


        private string promptText;
        public string PromptText {
            get { return this.promptText; }
            set {
                if (this.SetProperty(ref this.promptText, value))
                    SignaturePadConfiguration.Default.PromptText = value;
            }
        }


        private string captionText;
        public string CaptionText {
            get { return this.captionText; }
            set {
                if (this.SetProperty(ref this.captionText, value))
                    SignaturePadConfiguration.Default.CaptionText = value;
            }
        }


        private float strokeWidth;
        public float StrokeWidth {
            get { return this.strokeWidth; }
            set {
                if (this.SetProperty(ref this.strokeWidth, value))
                    SignaturePadConfiguration.Default.StrokeWidth = value;
            }
        }

        #endregion
    }
}
