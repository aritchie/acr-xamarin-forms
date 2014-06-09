using System;
using System.Threading;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class TextToSpeechViewModel : ViewModel {
        private CancellationTokenSource cancelSource;


        public TextToSpeechViewModel(ITextToSpeechService speech, IUserDialogService dialogs) {
            this.Speak = new Command(async () => {
        //            if (String.IsNullOrEmpty(this.Text)) 
        //                this.dialogService.Alert("Please enter the text!");
        //            else {
        //                using (this.cancelSource = new CancellationTokenSource()) {
        //                    using (this.dialogService.Loading("Speaking", () => this.cancelSource.Cancel(false))) { 
        //                        await this.SpeechService.Speak(this.Text, cancelToken: this.cancelSource.Token);
        //                    }
        //                } 
        //                this.cancelSource = null;
        //            }                
            });
            this.Cancel = new Command(async () => {
        //            if (this.cancelSource == null)
        //                this.dialogService.Alert("Nothing to cancel");
        //            else {
        //                this.cancelSource.Cancel();
        //                this.dialogService.Alert("Cancelled");
        //            }                
            });
        }


        private string text;
        public string Text {
            get { return this.text; }
            set { this.SetProperty(ref this.text, value); }
        }

        public ICommand Speak { get; private set; }
        public ICommand Cancel { get; private set; }
    }
}
