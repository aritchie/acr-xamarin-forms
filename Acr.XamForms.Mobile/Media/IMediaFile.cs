using System;
using System.IO;


namespace Acr.XamForms.Mobile.Media {
    
    public interface IMediaFile : IDisposable {

        string Path { get; }
        Stream GetStream();
    }
}
