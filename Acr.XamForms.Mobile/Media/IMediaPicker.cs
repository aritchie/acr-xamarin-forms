using System;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile.Media {
    
    public interface IMediaPicker {

        bool IsCameraAvailable { get; }
        bool IsPhotoGalleryAvailable { get; }
        bool IsVideoGalleryAvailable { get; }
        Task<IMediaFile> PickPhoto();
        Task<IMediaFile> TakePhoto(CameraOptions options = null);
        Task<IMediaFile> PickVideo();
        Task<IMediaFile> TakeVideo(VideoOptions config = null);
    }
}
