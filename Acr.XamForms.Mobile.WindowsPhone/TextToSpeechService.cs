using System;
using Windows.Phone.Speech.Synthesis;


namespace Acr.XamForms.Mobile.WindowsPhone {

    public class TextToSpeechService : ITextToSpeechService {
        private readonly SpeechSynthesizer speech;


        public TextToSpeechService() {
            this.speech = new SpeechSynthesizer();
        }


        public bool IsSpeaking { get; private set; }


        public async void Speak(string text) {
            if (this.IsSpeaking)
                return;

            this.IsSpeaking = true;
            try {
                // stop cancel exception
                await this.speech.SpeakTextAsync(text);
            }
            finally {
                this.IsSpeaking = false;
            }
        }


        public void Stop() {
            if (!this.IsSpeaking)
                return;

            this.speech.CancelAll();
            this.IsSpeaking = false;
        }
    }
}
