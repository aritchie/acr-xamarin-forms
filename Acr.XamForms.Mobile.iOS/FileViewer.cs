using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace Acr.XamForms.Mobile.iOS {
    
    public class FileViewer : IFileViewer {

        public bool Open(string fileName) {
            if (String.IsNullOrWhiteSpace(fileName))
                return false;

            //var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //var filePath = Path.Combine(path, "sample.pdf");
            //var viewer = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filePath));
            //viewer.PresentOpenInMenu(new RectangleF(0,-260,320,320),this.View, true);
            var url = NSUrl.FromFilename(fileName);

            if (url == null || !url.IsFileUrl)
                return false;

            if (!UIApplication.SharedApplication.CanOpenUrl(url))
                return false;

            UIApplication.SharedApplication.OpenUrl(url);
            return true;
        }
    }
}