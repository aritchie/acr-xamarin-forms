using System;
using Android.Media;


namespace Acr.XamForms.Mobile.Droid.TODO {

    public class AudioService : IAudioService {
        private readonly MediaPlayer player;

        public AudioService() {
            this.player = new MediaPlayer();
        }

        public SoundSource Source { get; private set; }

        public void Load(SoundSource source) {
            this.player.Reset();
            //this.player.SetDataSource("PATH");
            //this.player.SetAudioStreamType();
            this.player.Prepare();
        }


        public void Play() {
            this.player.Start();
        }


        public void Stop() {
            this.player.Stop();
        }

        public void Pause() {
            this.player.Pause();
        }
    }
}