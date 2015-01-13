using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.Mobile.Media;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class PhotoViewModel : ViewModel {
        private readonly IUserDialogService dialogs;


        public PhotoViewModel(IUserDialogService dialogs, IMediaPicker picker) {
            this.dialogs = dialogs;

            this.FromCamera = new Command(async () => {
                if (!picker.IsCameraAvailable)
                    dialogs.Alert("Camera is not available");
                else { 
                    var result = await picker.TakePhoto();
                    this.OnPhotoReceived(result);
                }
            });

            this.FromGallery = new Command(async () => {
                if (!picker.IsPhotoGalleryAvailable)
                    dialogs.Alert("Photo Gallery is unavailable");
                else { 
                    var result = await picker.PickPhoto();
                    this.OnPhotoReceived(result);
                }
            });

            this.Choice = new Command(() => dialogs.ActionSheet(new ActionSheetConfig()
                .Add("Camera", () => this.FromCamera.Execute(null))
                .Add("Gallery", () => this.FromGallery.Execute(null))
                .Add("Cancel")
            ));
        }


        public ICommand Choice { get; private set; }
        public ICommand FromGallery { get; private set; }
        public ICommand FromCamera { get; private set; }


        private ImageSource photoSource;
        public ImageSource Photo {
            get { return this.photoSource; }
            private set {
                this.photoSource = value;
                this.OnPropertyChanged();
            }
        }


        private void OnPhotoReceived(IMediaFile file) {
            if (file == null)
                this.dialogs.Alert("Photo Cancelled");
            else 
                this.Photo = ImageSource.FromFile(file.Path);
        }
    }
}
