using System;
using System.Linq;


namespace Acr.XamForms.Mobile.IO {
    
    public static class Extensions {

        public static IFile GetFile(this IDirectory directory, string fileName) {
            return directory
                .Files
                .FirstOrDefault(x => x.Name.Equals(fileName, StringComparison.Ordinal));
        }


        public static IFile GetOrCreateFile(this IDirectory directory, string fileName) {
            return directory.GetFile(fileName) ?? directory.CreateFile(fileName);
        }
    }
}
