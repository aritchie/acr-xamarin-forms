using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class TextToSpeechViewModel : ViewModel {

        public TextToSpeechViewModel(ITextToSpeechService speech, IUserDialogService dialogs) {
            this.Speak = new Command(() => {
                if (String.IsNullOrEmpty(this.Text)) 
                    dialogs.Alert("Please enter the text!");
                else 
                    speech.Speak(this.Text);
            });
            this.Cancel = new Command(speech.Stop);
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