using System;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile {
    
    public interface IPhotoService {

        bool IsCameraAvailable { get; }
        bool IsGalleryAvailable { get; }
        Task<PhotoResult> FromGallery();
        Task<PhotoResult> FromCamera();
    }
}
