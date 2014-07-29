using System;
using System.IO;


namespace Acr.XamForms.Mobile.Media {
    
    public abstract class AbstractOptions {

        public string FilePath { get; set; }


        public string GetFileName() {
            return String.IsNullOrWhiteSpace(this.FilePath)
                ? null
                : Path.GetFileName(this.FilePath);
        }


        public string GetDirectory() {
            return String.IsNullOrWhiteSpace(this.FilePath)
                ? null
                : Path.GetDirectoryName(this.FilePath);
        }
    }
}
