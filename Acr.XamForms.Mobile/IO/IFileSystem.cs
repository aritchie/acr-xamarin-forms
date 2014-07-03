using System;


namespace Acr.XamForms.Mobile.IO {
    
    public interface IFileSystem {

        IDirectory Local { get; }
        IDirectory Roaming { get; }

        IDirectory GetDirectory(string path);
        IFile GetFile(string path);
    }
}
