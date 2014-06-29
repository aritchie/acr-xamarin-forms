using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class PhotoViewModel : ViewModel {
        private readonly IUserDialogService dialogs;
        private readonly IPhotoService photos;


        public PhotoViewModel(IUserDialogService dialogs, IPhotoService photos) {
        //public PhotoViewModel() {
            //this.dialogs = DependencyService.Get<IUserDialogService>();
            //this.photos = DependencyService.Get<IPhotoService>();
            this.dialogs = dialogs;
            this.photos = photos;

            this.FromCamera = new Command(async () => {
                if (!photos.IsCameraAvailable)
                    dialogs.Alert("Camera is not available");
                else { 
                    var result = await photos.FromCamera();
                    this.OnPhotoReceived(result);
                }
            });

            this.FromGallery = new Command(async () => {
                if (!photos.IsGalleryAvailable)
                    dialogs.Alert("Photo Gallery is unavailable");
                else { 
                    var result = await photos.FromGallery();
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


        private string imagePath;
        public string ImagePath {
            get { return this.imagePath; }
            set {
                this.SetProperty(ref this.imagePath, value);
            }
        }


        private void OnPhotoReceived(PhotoResult result) {
            if (result.IsCancelled)
                this.dialogs.Alert("Photo Cancelled");
            else 
                this.ImagePath = result.Path;
        }
    }
}
