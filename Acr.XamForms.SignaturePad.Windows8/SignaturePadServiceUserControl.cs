//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Media;
//using SignaturePadControl = Xamarin.Controls.SignaturePad;
//using System.Linq;
//using System;
//using Xamarin.Forms;
//using Windows.Storage;
//using System.IO;
//using System.Threading.Tasks;

//namespace Acr.XamForms.SignaturePad.Windows8
//{
//    public partial class SignaturePadServiceUserControl : Windows.UI.Xaml.Controls.Page
//    {
//        private Windows.UI.Xaml.Controls.Grid content;
//        private SignaturePadControl signaturePad;
//        private Windows.UI.Xaml.Controls.Button saveButton;
//        private Windows.UI.Xaml.Controls.Button cancelButton;
//        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

//        public SignaturePadServiceUserControl(SignaturePadConfiguration config)
//        {
//            var signatureConfiguration = config;

//            //Main Content
//            content = new Windows.UI.Xaml.Controls.Grid();
//            content.Background = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.MainBackgroundColor));

//            #region SignaturePadControl Setup
//            signaturePad = new SignaturePadControl();
//            signaturePad.BackgroundColor = PlatformConverters.FormsToWindowsColor(signatureConfiguration.SignatureBackgroundColor);
//            signaturePad.Caption.Text = signatureConfiguration.CaptionText;
//            signaturePad.Caption.Foreground = PlatformConverters.BrushFromColor(signatureConfiguration.CaptionTextColor);
//            signaturePad.ClearLabel.Text = signatureConfiguration.ClearText;
//            signaturePad.ClearLabel.Foreground = PlatformConverters.BrushFromColor(signatureConfiguration.ClearTextColor);
//            signaturePad.SignaturePrompt.Text = signatureConfiguration.PromptText;
//            signaturePad.SignaturePrompt.Foreground = PlatformConverters.BrushFromColor(signatureConfiguration.PromptTextColor);
//            signaturePad.Stroke = new SolidColorBrush(PlatformConverters.FormsToWindowsColor(signatureConfiguration.StrokeColor));
//            signaturePad.StrokeWidth = (int)signatureConfiguration.StrokeWidth;
//            content.Children.Add(signaturePad);
//            #endregion

//            //Save Button Setup
//            saveButton = new Windows.UI.Xaml.Controls.Button();
//            saveButton.Content = signatureConfiguration.SaveText;
//            saveButton.VerticalAlignment = VerticalAlignment.Bottom;
//            saveButton.HorizontalAlignment = HorizontalAlignment.Right;
//            saveButton.Click += OnSave;
//            content.Children.Add(saveButton);

//            //Cancel Button Setup
//            cancelButton = new Windows.UI.Xaml.Controls.Button();
//            cancelButton.Content = signatureConfiguration.CancelText;
//            cancelButton.VerticalAlignment = VerticalAlignment.Bottom;
//            cancelButton.HorizontalAlignment = HorizontalAlignment.Left;
//            cancelButton.Click += OnCancel;
//            content.Children.Add(cancelButton);

//            //Set the content of the control
//            this.Content = content;
//        }

//        private SignatureService Resolve()
//        {
//            return DependencyService.Get<ISignatureService>() as SignatureService;
//        }

//        private async void OnSave(object sender, RoutedEventArgs args)
//        {
//            if (this.signaturePad.IsBlank)
//                return;

//            var points = this.signaturePad
//                .Points
//                .Select(x => new DrawPoint((float)x.X, (float)x.Y));

//            var service = this.Resolve();

//            using (var image = await this.signaturePad.GetImage(service.CurrentConfig.ImageType))
//            {
//                var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("signature.tmp", Windows.Storage.CreationCollisionOption.ReplaceExisting);
//                using (var memoryStream = new MemoryStream())
//                {
//                    image.CopyTo(memoryStream);
//                    await Windows.Storage.FileIO.WriteBytesAsync(file, memoryStream.ToArray());
//                }
//            }
//        }


//        private void OnCancel(object sender, RoutedEventArgs args)
//        {
//            this.Resolve().Cancel();
//            ((Windows.UI.Xaml.Controls.Frame)Window.Current.Content).BackStack.Clear();
//        }
//    }
//}
