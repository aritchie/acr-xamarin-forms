using System;
using Windows.System;


namespace Acr.XamForms.Mobile.WindowsPhone {
 
    public class FileViewer : IFileViewer {

        public bool Open(string fileName) {
            try { 
                Launcher.LaunchUriAsync(new Uri(fileName));
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
