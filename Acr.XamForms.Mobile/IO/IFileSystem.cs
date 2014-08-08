using System;


namespace Acr.XamForms.Mobile.IO {
    
    public interface IFileSystem {

        IDirectory AppData { get; }
        IDirectory Cache { get; }
        IDirectory Public { get; }
        IDirectory Temp { get; }

        IDirectory GetDirectory(string path);
        IFile GetFile(string path);
    }
}