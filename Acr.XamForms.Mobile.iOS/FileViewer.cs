using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace Acr.XamForms.Mobile.iOS {
    
    [Preserve]
    public class FileViewer : IFileViewer {

        public bool Open(string fileName) {
            if (String.IsNullOrWhiteSpace(fileName))
                return false;

            var url = NSUrl.FromFilename(fileName);
            if (url == null || !url.IsFileUrl)
                return false;

            var viewer = UIDocumentInteractionController.FromUrl(url);
            var view = Utils.GetTopView();

            viewer.PresentOpenInMenu(new RectangleF(0, -260, 320, 320), view, true);
            //if (!UIApplication.SharedApplication.CanOpenUrl(url))
            //    return false;

            //UIApplication.SharedApplication.OpenUrl(url);
            return true;
        }
    }
}