using System;
using System.Threading.Tasks;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class PhotoService : IPhotoService {

        #region IPhotoService Members

        public bool IsCameraAvailable {
            get { throw new NotImplementedException(); }
        }

        public bool IsGalleryAvailable {
            get { throw new NotImplementedException(); }
        }

        public Task<PhotoResult> FromGallery() {
            throw new NotImplementedException();
        }

        public Task<PhotoResult> FromCamera() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
