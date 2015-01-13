using System;
using AVFoundation;
using Foundation;


namespace Acr.XamForms.Mobile.iOS {

    [Preserve]
    public class TextToSpeechService : ITextToSpeechService {
        private readonly AVSpeechSynthesizer synthesizer = new AVSpeechSynthesizer();


        public bool IsSpeaking {
            get { return this.synthesizer.Speaking; }
        }


        public void Speak(string text) {
            using (var utterance = this.CreateSpeech(text)) 
                this.synthesizer.SpeakUtterance(utterance);
        }


        public void Stop() {
            this.synthesizer.StopSpeaking(AVSpeechBoundary.Immediate);
        }


        protected virtual AVSpeechUtterance CreateSpeech(string text) {
            return new AVSpeechUtterance(text) {
                PitchMultiplier = 1.0f,
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                //Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f
            };
        }
    }
}