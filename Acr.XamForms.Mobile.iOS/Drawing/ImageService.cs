using System;
using System.IO;


namespace Acr.XamForms.Mobile.iOS {
    
    public class ImageService : IImageService {

        public IImage Load(Stream stream) {
            throw new NotImplementedException();
        }


        public IImage Load(byte[] bytes) {
            throw new NotImplementedException();
        }


        public IImage Load(string path) {
            throw new NotImplementedException();
        }
    }


    public class iOSImage : IImage {

        internal iOSImage(Stream stream) {
            //UIImage.Fr
                //using (NSData data = image.AsJPEG((float)(_percentQuality / 100.0)))
                //{
                //    var byteArray = new byte[data.Length];
                //    Marshal.Copy(data.Bytes, byteArray, 0, Convert.ToInt32(data.Length));

                //    var imageStream = new MemoryStream();
                //    imageStream.Write(byteArray, 0, Convert.ToInt32(data.Length));
                //    imageStream.Seek(0, SeekOrigin.Begin);
                //}
        }

        public IImage Rotate(int degress) {
            throw new NotImplementedException();
        }

        public IImage Convert(ImageFormat imageFormat, int quality) {
            throw new NotImplementedException();
        }

        public int Width {
            get { throw new NotImplementedException(); }
        }

        public int Height {
            get { throw new NotImplementedException(); }
        }


        public void Dispose() {
        }
    }
}