using System;
using System.IO;
using Acr.XamForms.Mobile.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystem))]


namespace Acr.XamForms.Mobile.IO {

    public class FileSystem : IFileSystem {

        public FileSystem() {
#if WINDOWS_PHONE
            // TODO: this.Local = new Directory(System.Windows.Forms.Application.LocalUserAppDataPath);
#elif __IOS__
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var localAppData = Path.Combine(documents, "..", "Library");
            this.Local = new Directory(localAppData);
#elif __ANDROID__
            Local = new Directory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
#endif
        }

        public IDirectory Local { get; private set; }
        public IDirectory Roaming { get; private set; }


        public IDirectory GetDirectory(string path) {
            return new Directory(new DirectoryInfo(path));
        }


        public IFile GetFile(string fileName) {
            return new File(new FileInfo(fileName));
        }
    }
}