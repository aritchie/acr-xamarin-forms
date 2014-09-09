using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Media;
using XamCamDevice = Xamarin.Media.CameraDevice;
using XamVideoQuality = Xamarin.Media.VideoQuality;
using XamMediaPicker = Xamarin.Media.MediaPicker;


namespace Acr.XamForms.Mobile.Media {

    public class MediaPicker : IMediaPicker {
        private readonly XamMediaPicker picker;


        public MediaPicker() {
#if __IOS__ || WINDOWS_PHONE
            this.picker = new XamMediaPicker();
#elif __ANDROID__
            this.picker = new XamMediaPicker(Forms.Context);
#endif            
        }


        public bool IsPhotoGalleryAvailable {
            get { return this.picker.PhotosSupported; }
        }


        public bool IsCameraAvailable {
            get { return this.picker.IsCameraAvailable; }
        }


        public bool IsVideoGalleryAvailable {
            get { return this.picker.VideosSupported; }
        }


        public async Task<IMediaFile> PickPhoto() {
            if (!this.IsPhotoGalleryAvailable)
                throw new ArgumentException("Photo gallery is not available");

            try { 
                var file = await this.picker.PickPhotoAsync();
                return new MediaFile(file);
            }
            catch (OperationCanceledException) {
                return null;
            }
        }


        public async Task<IMediaFile> TakePhoto(CameraOptions options) {
            if (!this.IsCameraAvailable)
                throw new ArgumentException("Camera is not available");

            options = options ?? new CameraOptions();
            try {
                var file = await this.picker.TakePhotoAsync(new StoreCameraMediaOptions {
                    Directory = options.GetDirectory(),
                    Name = options.GetFileName(),
                    DefaultCamera = (XamCamDevice)Enum.Parse(typeof(XamCamDevice), options.Camera.ToString())
                });
                return new MediaFile(file);
            }
            catch (OperationCanceledException) {
                return null;
            }
        }


        public async Task<IMediaFile> PickVideo() {
            if (!this.IsVideoGalleryAvailable)
                throw new ArgumentException("Video gallery is not available");

            try {
                var file = await this.picker.PickVideoAsync();
                return new MediaFile(file);
            }
            catch (OperationCanceledException) {
                return null;
            }
        }


//protected override void OnCreate (Bundle bundle)
//{
//    var picker = new MediaPicker (this);
//    if (!picker.IsCameraAvailable)
//        Console.WriteLine ("No camera!");
//    else {
//        var intent = picker.GetTakePhotoUI (new StoreCameraMediaOptions {
//            Name = "test.jpg",
//            Directory = "MediaPickerSample"
//        });
//        StartActivityForResult (intent, 1);
//    }
//}

//protected override async void OnActivityResult (int requestCode, Result resultCode, Intent data)
//{
//    // User canceled
//    if (resultCode == Result.Canceled)
//        return;

//    MediaFile file = await data.GetMediaFileExtraAsync (this);
//    Console.WriteLine (file.Path);
//}

        public async Task<IMediaFile> TakeVideo(VideoOptions options) {
            if (!this.IsCameraAvailable)
                throw new ArgumentException("Camera is not available");

            options = options ?? new VideoOptions();
            try {
                var file = await this.picker.TakeVideoAsync(new StoreVideoOptions {
                    Directory = options.GetDirectory(),
                    Name = options.GetFileName(),
                    DesiredLength = options.DesiredLength,
                    DefaultCamera = (XamCamDevice)Enum.Parse(typeof(XamCamDevice), options.Camera.ToString()),
                    Quality = (XamVideoQuality)Enum.Parse(typeof(XamVideoQuality), options.Quality.ToString())
                });
                return new MediaFile(file);
            }
            catch (OperationCanceledException) {
                return null;
            }           
        }
    }
}
