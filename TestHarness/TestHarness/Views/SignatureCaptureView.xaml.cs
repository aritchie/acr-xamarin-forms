using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHarness.ViewModels;
using Xamarin.Forms;
using Acr.XamForms.SignaturePad;

namespace TestHarness.Views
{
    public partial class SignatureCaptureView : ContentPage
    {
        SignatureCaptureViewModel _viewModel;

        public SignatureCaptureView(SignatureCaptureViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public SignatureCaptureViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        public async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (!signaturePad.IsBlank)
            {
                using (var sigStream = signaturePad.GetImage(ImageFormatType.Png))
                {
                    sigStream.Position = 0;
                    var ms = new MemoryStream();
                    sigStream.CopyTo(ms);
                    byte[] sigBytes = ms.ToArray();
                    _viewModel.SaveSignature(sigBytes);
                }

                await Navigation.PopModalAsync();
            }
            else {
                await DisplayAlert("Signature", "The signature cannot be empty.", "OK");
            }
        }
    }
}
