using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Phone.Speech.Synthesis;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class TextToSpeechService : ITextToSpeechService {

        public bool IsSpeaking { get; private set; }


        public async Task Speak(string text, CancellationToken cancelToken) {
            using (var synth = new SpeechSynthesizer()) {
                cancelToken.Register(() => {
                    if (this.IsSpeaking)
                        synth.CancelAll();
                });
                //this.SetVoice(synth, options);

                this.IsSpeaking = true;
                try { 
                    // stop cancel exception
                    await synth.SpeakTextAsync(text);
                }
                catch { }
                this.IsSpeaking = false;
            }
        }
    }
}
