using System;
using System.IO;
using Android.Content;
using Android.Webkit;
using Xamarin.Forms;


namespace Acr.XamForms.Mobile.Droid {
    
    public class FileViewer : IFileViewer {
        private readonly string externalDirectory;


        public FileViewer() {
            this.externalDirectory = Forms.Context.ApplicationContext.GetExternalFilesDir(null).AbsolutePath;
        }


        public bool Open(string fileName) {
            if (String.IsNullOrWhiteSpace(fileName))
                return false;

            var dataType = GetMimeType(fileName);

            // external apps do not have access to cache directory, copy from the cache to an external location
            var newPath = Path.Combine(
                this.externalDirectory,
                Path.GetFileName(fileName)
            );
            File.Copy(fileName, newPath, true);

            try {
                var file = new Java.IO.File(newPath);
                var uri = Android.Net.Uri.FromFile(file);
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, dataType);
                Forms.Context.StartActivity(intent);
                return true;
            }
            catch {
                return false;
            }
        }


        private static string GetMimeType(string fileName) {
            var ext = Path.GetExtension(fileName);
            var mime = "";
            if (ext != null) 
                mime = MimeTypeMap.Singleton.GetMimeTypeFromExtension(ext);

            return mime;
        }
    }
}