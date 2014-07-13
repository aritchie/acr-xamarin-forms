using System;
using MonoTouch.AudioToolbox;


namespace Acr.XamForms.Mobile.iOS {
    
    public class AudioService : IAudioService {
        private SystemSound sound;


        public SoundSource Source {
            get { throw new NotImplementedException(); }
        }


        public void Load(SoundSource source) {
            // URL OR FILE
            //this.sound = SystemSound.FromFile("Sounds/tap.aif");
        }


        public void Play() {
        }

        public void Stop() {
            throw new NotImplementedException();
        }

        public void Pause() {
            throw new NotImplementedException();
        }
    }
}
/*IOS
//holds the sound to play
        private SystemSound Sound;
        
        //prepares the audio
        public override void ViewDidLoad () {
            base.ViewDidLoad ();
            
            //enable audio
            AudioSession.Initialize();
            
            //load the sound
            
            
        }
        
        partial void playSystemSound(NSObject sender) {
            Sound.PlaySystemSound(); 
        }
        
        partial void playAlertSound (NSObject sender) {
            Sound.PlayAlertSound();
        }

        partial void vibrate (NSObject sender) {
            SystemSound.Vibrate.PlaySystemSound();
        }
        
 * 
 * ANDROID

*/