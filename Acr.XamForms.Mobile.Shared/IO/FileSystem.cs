using System;
using System.IO;
using Acr.XamForms.Mobile.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystem))]


namespace Acr.XamForms.Mobile.IO {

    public class FileSystem : IFileSystem {

        public FileSystem() {
#if WINDOWS_PHONE
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            this.AppData = new Directory(path);
            this.Cache = new Directory(Path.Combine(path, "Cache"));
            this.Public = new Directory(Path.Combine(path, "Public"));
            this.Temp = new Directory(Path.Combine(path, "Temp"));
#elif __IOS__
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var library = Path.Combine(documents, "..", "Library");
            this.AppData = new Directory(library);
            this.Cache = new Directory(Path.Combine (library, "Caches"));
            this.Temp = new Directory(Path.Combine(documents, "..", "tmp"));
            this.Public = new Directory(documents);
#elif __ANDROID__            
            this.AppData = new Directory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            this.Cache = new Directory(Android.App.Application.Context.CacheDir.AbsolutePath);
            this.Temp = new Directory(Android.App.Application.Context.CacheDir.AbsolutePath);
            this.Public = new Directory(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath);
#endif
        }

        public IDirectory AppData { get; private set; }
        public IDirectory Cache { get; private set; }
        public IDirectory Public { get; private set; }
        public IDirectory Temp { get; private set; }


        public IDirectory GetDirectory(string path) {
            return new Directory(new DirectoryInfo(path));
        }


        public IFile GetFile(string fileName) {
            return new File(new FileInfo(fileName));
        }
    }
}