using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Media;


namespace Acr.XamForms.Mobile {

    public class PhotoService : IPhotoService {

        public PhotoService() {
            var picker = this.CreateMediaPicker();
            this.IsCameraAvailable = picker.IsCameraAvailable && picker.PhotosSupported;
            this.IsGalleryAvailable = picker.PhotosSupported;
        }
#if __IOS__
        private MediaPicker CreateMediaPicker() {
            return new MediaPicker();
        }
#elif __ANDROID__
        private MediaPicker CreateMediaPicker() {
            return new MediaPicker(Forms.Context);
        }
#elif WINDOWSPHONE

#endif

        public bool IsGalleryAvailable { get; protected set; }
        public bool IsCameraAvailable { get; protected set; }


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
