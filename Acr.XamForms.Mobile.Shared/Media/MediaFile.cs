using System;
using System.IO;


namespace Acr.XamForms.Mobile.Media {

    public class MediaFile : IMediaFile {
        private readonly Xamarin.Media.MediaFile file;


        public MediaFile(Xamarin.Media.MediaFile file) {
            this.file = file;
        }


        public string Path {
            get { return this.file.Path; }
        }


        public Stream GetStream() {
            return this.file.GetStream();
        }


        public void Dispose() {
            this.file.Dispose();
        }
    }
}
