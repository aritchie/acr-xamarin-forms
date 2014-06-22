using System;


namespace Acr.XamForms.Mobile {
    
    public interface ITextToSpeechService {

        bool IsSpeaking { get; }
        void Speak(string text);
        void Stop();
    }
}
