using System;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.AVFoundation;
using MonoTouch.Foundation;


namespace Acr.XamForms.Mobile.iOS {
    
    [Preserve]
    public class TextToSpeechService : ITextToSpeechService {

        public bool IsSpeaking { get; private set; }

        public async Task Speak(string text, CancellationToken cancelToken) {
            this.IsSpeaking = true;
            using (var speech = new AVSpeechSynthesizer()) {
                using (var utterance = this.CreateSpeech(text)) {
                    cancelToken.Register(() => {
                        if (this.IsSpeaking) { 
                            speech.StopSpeaking(AVSpeechBoundary.Immediate);
                            this.IsSpeaking = false;
                        }
                    });
                    speech.SpeakUtterance(utterance);
                }
            }
            this.IsSpeaking = false;
        }


        protected virtual AVSpeechUtterance CreateSpeech(string text) {
            return new AVSpeechUtterance(text);

            //if (opts.Voice != null) { 
            //    var voice = AVSpeechSynthesisVoice
            //        .GetSpeechVoices()
            //        .FirstOrDefault(x => x.Description == opts.Voice.Id);

            //    utt.Voice = voice;
            //}

            //if (opts.SpeechRate > 0) {
            //    utt.Rate = opts.SpeechRate;
            //}

            //if (opts.VoicePitch > 0) {
            //    utt.PitchMultiplier = opts.VoicePitch;
            //}
            //return utt;
        }
    }
}