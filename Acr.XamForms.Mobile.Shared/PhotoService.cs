using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Media;


namespace Acr.XamForms.Mobile {

    public class PhotoService : IPhotoService {
        private bool isLoaded;
 

#if __IOS__ || WINDOWS_PHONE
        private MediaPicker CreateMediaPicker() {
            return new MediaPicker();
        }
#elif __ANDROID__
        private MediaPicker CreateMediaPicker() {
            return new MediaPicker(Android.App.Application.Context);
        }
#endif

        private void AssertLoad() {
            if (this.isLoaded)
                return;

            var picker = this.CreateMediaPicker();
            this.isCameraAvailable = picker.IsCameraAvailable && picker.PhotosSupported;
            this.isGalleryAvailable = picker.PhotosSupported;
        }


        private bool isGalleryAvailable;
        public bool IsGalleryAvailable {
            get {
                this.AssertLoad();
                return this.isGalleryAvailable;
            }
        }


        private bool isCameraAvailable;
        public bool IsCameraAvailable {
            get {
                this.AssertLoad();
                return this.isCameraAvailable;
            }
        }


        public async Task<PhotoResult> FromGallery() {
            var picker = this.CreateMediaPicker();
            if (!this.IsGalleryAvailable)
                throw new Exception("No camera is available");

            try { 
                using (var file = await picker.PickPhotoAsync())
                    return new PhotoResult(file.Path, File.OpenRead);
            }
            catch (OperationCanceledException) {
                return PhotoResult.Cancelled;
            }
        }


        public async Task<PhotoResult> FromCamera() {
            var picker = this.CreateMediaPicker();
            if (!picker.IsCameraAvailable)
                throw new Exception("No camera is available");

            try {
                using (var file = await this.UseCamera(picker)) 
                    return new PhotoResult(file.Path, File.OpenRead);
            }
            catch (OperationCanceledException) {
                return PhotoResult.Cancelled;
            }
        }


        private async Task<MediaFile> UseCamera(MediaPicker picker) {
            var fileName = DateTime.Now.Ticks + ".png";
            return await picker.TakePhotoAsync(new StoreCameraMediaOptions {
                Directory = "temp",
                Name = fileName
                //DefaultCamera = CameraDevice.Rear
            });
        }
    }
}
