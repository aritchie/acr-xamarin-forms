using System;
using System.IO;
using Acr.XamForms.Mobile.Droid;
using Android.Graphics;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageService))]


namespace Acr.XamForms.Mobile.Droid {
    
    public class ImageService : IImageService {

        public IImage Load(Stream stream) {
            return new DroidImage(stream);
        }


        public IImage Load(byte[] bytes) {
            return this.Load(new MemoryStream(bytes));
        }


        public IImage Load(string path) {
            return this.Load(File.OpenRead(path));
        }
    }


    public class DroidImage : IImage {
        private Bitmap bitmap;


        internal DroidImage(Stream stream) {
            this.bitmap = BitmapFactory.DecodeStream(stream);
        }


        public int Height {
            get { return this.bitmap.Height; }
        }


        public int Width {
            get { return this.bitmap.Width; }
        }


        public IImage Rotate(int degrees) {
            using (var matrix = new Matrix()) {
                matrix.PreRotate(degrees);
                var bmp = Bitmap.CreateBitmap(this.bitmap, 0, 0, this.bitmap.Width, this.bitmap.Height, matrix, true);
                this.bitmap.Dispose();
                this.bitmap = bmp;
            }            
            return this;
        }

        
        public IImage Convert(ImageFormat imageFormat, int quality) {
            var ms = new MemoryStream();
            this.bitmap.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
            ms.Seek(0, SeekOrigin.Begin);
            this.bitmap.Dispose();
            this.bitmap = BitmapFactory.DecodeStream(ms);
            return this;
        }


        public void Dispose() {
            this.bitmap.Dispose();
        }
    }
}