using System;
using Acr.XamForms.Mobile.Media;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class VideoViewModel : ViewModel {
        private readonly IMediaPicker picker;
        private readonly IUserDialogService dialogs;


        public VideoViewModel(IMediaPicker picker, IUserDialogService dialogs) {
            this.picker = picker;
            this.dialogs = dialogs;
        }
    }
}
