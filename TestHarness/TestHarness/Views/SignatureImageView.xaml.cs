using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHarness.ViewModels;
using Xamarin.Forms;

namespace TestHarness.Views
{
    public partial class SignatureImageView : ContentPage
    {
        public SignatureImageView()
        {
            InitializeComponent();

            var viewModel = new SignatureImageViewModel();
            BindingContext = viewModel;

            viewModel.SignatureChanged += (sender, args) => {
                signatureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(args.Image));
            };

            if (viewModel.SignatureImage != null)
            {
                signatureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(viewModel.SignatureImage));
            }
        }
    }
}
