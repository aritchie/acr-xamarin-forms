using System;
using System.Collections.Generic;


namespace Acr.XamForms.Mobile.IO {
    
    public interface IDirectory {

        string Name { get; }
        string FullName { get; }
        bool Exists { get; }

        IDirectory Root { get; }
        IDirectory Parent { get; }

        DateTime CreationTime { get; }
        DateTime LastAccessTime { get; }
        DateTime LastWriteTime { get; }

        void Create();        
        void MoveTo(string path);
        void Delete(bool recursive = false);

        bool FileExists(string fileName);
        IFile CreateFile(string name);
        IDirectory CreateSubdirectory(string name);
        IEnumerable<IDirectory> Directories { get; } 
        IEnumerable<IFile> Files { get; }
    }
}
