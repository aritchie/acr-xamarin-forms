using System;
using System.IO;


namespace Acr.XamForms.Mobile {

    public class PhotoResult {

        public static readonly PhotoResult Cancelled = new PhotoResult();
        private readonly Func<string, Stream> getStream; 

        public bool IsCancelled { get; private set; }
        public string Path { get; private set; }


        private PhotoResult() {
            this.IsCancelled = true;
        }


        public PhotoResult(string path, Func<string, Stream> getStream) {
            this.Path = path;
            this.getStream = getStream;
        }


        public Stream GetStream() {
            if (getStream == null)
                throw new Exception("Stream is not available");

            return getStream(this.Path);
        }
    }
}
