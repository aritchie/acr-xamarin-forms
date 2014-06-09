using System;
using System.Threading;
using System.Threading.Tasks;

namespace Acr.XamForms.Mobile {
    
    public interface ITextToSpeechService {

        bool IsSpeaking { get; }
        Task Speak(string text, CancellationToken cancelToken);
    }
}
