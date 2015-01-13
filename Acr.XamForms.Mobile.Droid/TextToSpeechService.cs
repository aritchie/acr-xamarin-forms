using System;
using Android.Speech.Tts;
using Xamarin.Forms;


namespace Acr.XamForms.Mobile.Droid {

    public class TextToSpeechService : Java.Lang.Object, ITextToSpeechService, TextToSpeech.IOnInitListener {
        private readonly TextToSpeech speech;
        public bool IsInitialized { get; private set; }


        public TextToSpeechService() {
			this.speech = new TextToSpeech(Forms.Context.ApplicationContext, this);
        }


        public bool IsSpeaking { get; private set; }


        public void Speak(string text) {
            if (this.IsSpeaking || !this.IsInitialized)
                return;
				
            this.IsSpeaking = true;
			try {
            	this.speech.Speak(text, QueueMode.Flush, null);
			}
			finally {
				this.IsSpeaking = false;
			}
        }


        public void Stop() {
            if (!this.IsSpeaking)
                return;

			try {
            	this.speech.Stop();
			}
			finally {
				this.IsSpeaking = false;
			}
        }


        public void OnInit(OperationResult status) {
			this.IsInitialized = (status == OperationResult.Success);
        }
    }
}