using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Linq;
using System;
using Xamarin.Forms;
using Windows.Storage;
using System.IO;
using SignaturePadControl = Xamarin.Controls.SignaturePad;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

namespace Acr.XamForms.SignaturePad.Windows8
{
    public partial class SignaturePadServicePage : Windows.UI.Xaml.Controls.Page
    {
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        private Windows.Storage.StorageFile file;
        private SignaturePadControl signaturePad;
        private Windows.UI.Xaml.Controls.Frame frame
        {
            get
            {
                return ((Windows.UI.Xaml.Controls.Frame)Window.Current.Content);
            }
        }

        public SignaturePadServicePage()
        {
            InitializeComponent();

            var signatureConfiguration = this.Resolve().CurrentConfig;

            //Main Content
            LayoutRoot.Background = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(Color.Black));

            #region SignaturePadControl Setup
            signaturePad = new SignaturePadControl();
            signaturePad.BackgroundColor = PlatformConverters.FormsToWindowsColor(signatureConfiguration.SignatureBackgroundColor);
            signaturePad.Caption.Text = signatureConfiguration.CaptionText;
            signaturePad.Caption.Foreground = PlatformConverters.BrushFromColor(signatureConfiguration.CaptionTextColor);
            signaturePad.ClearLabel.Text = signatureConfiguration.ClearText;
            signaturePad.ClearLabel.Foreground = PlatformConverters.BrushFromColor(signatureConfiguration.ClearTextColor);
            signaturePad.SignaturePrompt.Text = signatureConfiguration.PromptText;
            signaturePad.SignaturePrompt.Foreground = PlatformConverters.BrushFromColor(signatureConfiguration.PromptTextColor);
            signaturePad.Stroke = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.StrokeColor));
            signaturePad.StrokeWidth = (int)signatureConfiguration.StrokeWidth;
            signaturePad.Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 39);
            LayoutRoot.Children.Insert(0, (signaturePad));
            #endregion

            //Save Button Setup
            SaveButton.Content = signatureConfiguration.SaveText;
            SaveButton.Foreground = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.SaveTextColor));
            SaveButton.Background = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.SaveButtonColor));

            //Cancel Button Setup
            CancelButton.Content = signatureConfiguration.CancelText;
            CancelButton.Foreground = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.CanceTextColor));
            CancelButton.Background = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.CanceButtonColor));
        }

        private SignatureService Resolve()
        {
            return DependencyService.Get<ISignatureService>() as SignatureService;
        }

        private void OnSave(object sender, RoutedEventArgs args)
        {
            if (signaturePad.IsBlank)
                return;

            var points = signaturePad.Points.Select(x => new DrawPoint((float)x.X, (float)x.Y));

            var service = this.Resolve(); //Grab the signature service

            using (var image = this.signaturePad.GetImage(service.CurrentConfig.ImageType))
            {
                //Create new memory stream
                using (var memoryStream = new MemoryStream())
                {
                    //Copy the image buffer stream to the memory stream
                    image.CopyTo(memoryStream);

                    this.Resolve().Complete(
                        new SignatureResult(
                           false,
                           () =>
                           {  
                               //Return a new readable memory stream from the byteArray of the current memory stream 
                               return new MemoryStream(memoryStream.ToArray());
                           },
                           points
                       )
                    );
                }
            }
            frame.GoBack();
        }

        private void OnCancel(object sender, RoutedEventArgs args)
        {
            this.Resolve().Cancel();
            frame.GoBack();
        }
    }
}
