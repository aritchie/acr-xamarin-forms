using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness.ViewModels
{
    public class SignatureCaptureViewModel
    {
        public event ImagePropertyChangedEventHandler SignatureCaptured;

        public SignatureCaptureViewModel()
        {
        }

        public void SaveSignature(byte[] signature)
        {
            if (SignatureCaptured != null)
            {
                SignatureCaptured(this, new ImagePropertyChangedEventArgs(signature));
            }
        }
    }

    // TODO: May want to refactor customer event args/handlers by moving to a different location
    public class ImagePropertyChangedEventArgs : EventArgs
    {
        byte[] _image;

        public ImagePropertyChangedEventArgs(byte[] image)
        {
            _image = image;
        }

        public byte[] Image
        {
            get { return _image; }
        }
    }

    public delegate void ImagePropertyChangedEventHandler(object sender, ImagePropertyChangedEventArgs args);
}
