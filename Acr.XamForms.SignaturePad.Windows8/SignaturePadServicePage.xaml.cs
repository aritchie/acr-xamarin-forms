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

        private async void OnSave(object sender, RoutedEventArgs args)
        {
            if (signaturePad.IsBlank)
                return;

            var points = signaturePad.Points.Select(x => new DrawPoint((float)x.X, (float)x.Y));

            var service = this.Resolve();

            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                using (var image = await this.signaturePad.GetImage(service.CurrentConfig.ImageType))
                {
                    file = await localFolder.CreateFileAsync("signature.tmp", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    using (var memoryStream = new MemoryStream())
                    {
                        image.CopyTo(memoryStream);
                        await Windows.Storage.FileIO.WriteBytesAsync(file, memoryStream.ToArray());
                    }
                }

                this.Resolve().Complete(
                    new SignatureResult(
                        false,
                        () =>
                        {
                            var task = file.OpenStreamForReadAsync();
                            task.Wait();
                            return task.Result;
                        },
                        points
                    )
                );
                frame.GoBack();
            });
        }

        private void OnCancel(object sender, RoutedEventArgs args)
        {
            this.Resolve().Cancel();
            frame.GoBack();
        }
    }
}
