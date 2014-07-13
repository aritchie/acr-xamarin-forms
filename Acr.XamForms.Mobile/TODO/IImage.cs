using System;
using System.IO;


namespace Acr.XamForms.Mobile {
    
    public enum ImageFormat {
        Png,
        Jpg
    }


    public interface IImage : IDisposable {

        IImage Rotate(int degress);
        IImage Convert(ImageFormat imageFormat, int quality);

        int Width { get; }
        int Height { get; }
    }
}
