using System;


namespace Acr.XamForms.Mobile {

    public class SoundSource {
        // TODO: properties - length, name
    }
    
    public interface IAudioService {

        SoundSource Source { get; }
        // void Record();
        //bool IsPlaying { get; }
        //void Seek(int position)
        // TODO: current play position
        void Load(SoundSource source);
        void Play();
        void Stop();
        void Pause();
    }
}