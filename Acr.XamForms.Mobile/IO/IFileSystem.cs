using System;


namespace Acr.XamForms.Mobile.IO {
    
    public interface IFileSystem {

        IDirectory AppData { get; }
        //IDirectory Roaming { get; }

        IDirectory GetDirectory(string path);
        IFile GetFile(string path);
    }
}
