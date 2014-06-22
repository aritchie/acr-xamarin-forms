using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.Speech.Tts;
using Xamarin.Forms;


namespace Acr.XamForms.Mobile.Droid {
    
    public class TextToSpeechService : Java.Lang.Object, ITextToSpeechService, TextToSpeech.IOnInitListener {
        private TextToSpeech speech;
        private string speechText;


        public bool IsSpeaking { get; private set; }


        public void Speak(string text) {
            if (this.IsSpeaking)
                return;

            this.IsSpeaking = true;
            this.speechText = text;
            if (this.speech != null) 
                this.DoSpeech();
            else 
                this.speech = new TextToSpeech(Forms.Context, this);
        }


        public void Stop() {
            if (!this.IsSpeaking)
                return;

            this.speech.Stop();
        }


        private void DoSpeech() {
            this.speech.Speak(this.speechText, QueueMode.Flush, new Dictionary<string, string>());
            this.IsSpeaking = false;
        }


        public void OnInit(OperationResult status) {
            if (status == OperationResult.Success)
                this.DoSpeech();
        }


        public IntPtr Handle {
            get { return IntPtr.Zero; }
        }


        public void Dispose() {
        }
    }
}