using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestHarness.ViewModels
{
    public class SignatureImageViewModel : INotifyPropertyChanged
    {
        public event EventHandler SignatureCaptureRequested;
        public event ImagePropertyChangedEventHandler SignatureChanged;

        public SignatureImageViewModel()
        {
            SignatureCommand = new Command(() => {
                Debug.WriteLine("Signature pad requested...");
                if (SignatureCaptureRequested != null)
                {
                    SignatureCaptureRequested(this, EventArgs.Empty);
                }
            });
        }

        private byte[] _b64Signature;

        public byte[] SignatureImage
        {
            get
            {
                return _b64Signature;
            }

            set
            {
                _b64Signature = value;
                if (SignatureChanged != null)
                {
                    SignatureChanged(this, new ImagePropertyChangedEventArgs(_b64Signature));
                }
            }
        }

        public ICommand SignatureCommand
        {
            protected set;
            get;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = null)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
