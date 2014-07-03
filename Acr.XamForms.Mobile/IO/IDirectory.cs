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
        //IFile CreateFile(string name, bool overwriteIfExists);
        void MoveTo(string path);
        IDirectory CreateSubdirectory(string name);
        void Delete(bool recursive = false);

        IEnumerable<IDirectory> Directories { get; } 
        IEnumerable<IFile> Files { get; }
    }
}
