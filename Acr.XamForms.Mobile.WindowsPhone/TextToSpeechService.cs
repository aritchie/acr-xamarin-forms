using System;
using Windows.Phone.Speech.Synthesis;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class TextToSpeechService : ITextToSpeechService {
        private SpeechSynthesizer speech;

        public bool IsSpeaking { get; private set; }


        public async void Speak(string text) {
            if (this.IsSpeaking)
                return;


            using (this.speech = new SpeechSynthesizer()) {
                this.IsSpeaking = true;
                try { 
                    // stop cancel exception
                    await this.speech.SpeakTextAsync(text);
                }
                catch { }
                this.IsSpeaking = false;
            }
        }


        public void Stop() {
            if (!this.IsSpeaking && this.speech != null)
                return;

            this.speech.CancelAll();
            this.IsSpeaking = false;
        }
    }
}
