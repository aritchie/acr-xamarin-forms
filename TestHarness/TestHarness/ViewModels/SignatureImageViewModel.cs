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
using Acr.XamForms.SignaturePad;
using Acr.UserDialogs;
using System.IO;

namespace TestHarness.ViewModels
{
    public class SignatureImageViewModel : INotifyPropertyChanged
    {
        public event ImagePropertyChangedEventHandler SignatureChanged;
		private ISignatureService _sigService;

        public SignatureImageViewModel()
        {
			_sigService = DependencyService.Get<ISignatureService> ();

            SignatureCommand = new Command(async (obj) => {
                Debug.WriteLine("Signature pad requested...");
				var result = await _sigService.Request(new SignaturePadConfiguration() {
					StrokeColor = Color.Blue,
					StrokeWidth = Device.OnPlatform<int>(2, 4, 4),
					ClearTextColor = Color.Red,
					PromptTextColor = Color.Red,
					SignatureLineColor = Color.Red,
					CaptionText = "Rotate for larger signing area",
					CaptionTextColor = Color.Black
				});

				if (result.Cancelled)
					await UserDialogs.Instance.AlertAsync("Signature Cancelled");
				else {
					using (var sigStream = result.GetStream())
					{
						sigStream.Position = 0;
						var ms = new MemoryStream();
						sigStream.CopyTo(ms);
						byte[] sigBytes = ms.ToArray();
						SignatureImage = sigBytes;
					}
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
