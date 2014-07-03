using System;
using System.IO;


namespace Acr.XamForms.Mobile.IO {
    
    public interface IFile {

        string Name { get; }
        string FullName { get; }
        string Extension { get; }
        long Length { get; }
        bool Exists { get; }
        string MimeType { get; }

        Stream Create();
        Stream OpenRead();
        Stream OpenWrite();
        void MoveTo(string path);
        IFile CopyTo(string path);
        void Delete();

        IDirectory Directory { get; }
        DateTime LastAccessTime { get; }
        DateTime LastWriteTime { get; }
        DateTime CreationTime { get; }
    }
}
