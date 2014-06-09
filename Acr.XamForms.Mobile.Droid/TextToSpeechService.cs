using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.Speech.Tts;
using Xamarin.Forms;


namespace Acr.XamForms.Mobile.Droid {
    
    public class TextToSpeechService : ITextToSpeechService, TextToSpeech.IOnInitListener {
        private TaskCompletionSource<object> tcs;
 

        public bool IsSpeaking { get; private set; }


        public Task Speak(string text, CancellationToken cancelToken) {
            this.tcs = new TaskCompletionSource<object>();

            using (var tts = new TextToSpeech(Forms.Context, this)) {
                cancelToken.Register(() => {
                    this.IsSpeaking = false;
                    tts.Stop();
                    this.tcs.TrySetCanceled();
                });
                //tts.SetSpeechRate(opts.SpeechRate);
                //tts.SetPitch(opts.VoicePitch);
                this.IsSpeaking = true;
                tts.Speak(text, QueueMode.Flush, new Dictionary<string, string>());
            }
            return this.tcs.Task;
        }
    

        public void OnInit(OperationResult status) {
           if (this.tcs == null)
                return;

            switch (status) {
                case OperationResult.Error:
                    this.tcs.TrySetException(new ArgumentException("Error starting TTS engine"));
                    break;

                case OperationResult.Success:
                    this.tcs.TrySetResult(null);
                    break;
            }
            this.IsSpeaking = false;
        }


        public IntPtr Handle {
            get { return IntPtr.Zero; }
        }


        public void Dispose() {
        }
    }
}