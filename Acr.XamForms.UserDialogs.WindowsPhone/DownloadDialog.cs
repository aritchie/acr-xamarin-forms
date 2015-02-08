using System.Windows;
using Microsoft.Phone.Shell;

namespace Acr.XamForms.UserDialogs.WindowsPhone
{
    public class DownloadDialog : IDownloadDialog
    {
        private ProgressIndicator _progress;

        public DownloadDialog(string message)
        {
            Message = message;
            _progress = new ProgressIndicator
            {
                IsVisible = true,
                IsIndeterminate = true,
                Text = Message
            };
            SystemTray.SetProgressIndicator(Deployment.Current, _progress);
        }

        public void Dispose()
        {
            _progress.IsVisible = false;
            SystemTray.SetProgressIndicator(Deployment.Current, null);
        }

        public string Message { get; set; }

        public bool IsShowing
        {
            get { return _progress != null && _progress.IsVisible; }
        }

        public void Show()
        {
            _progress = new ProgressIndicator
            {
                IsVisible = true,
                IsIndeterminate = true,
                Text = Message
            };
            SystemTray.SetProgressIndicator(Deployment.Current, _progress);
        }

        public void Hide()
        {
            _progress = new ProgressIndicator
            {
                IsVisible = true,
                IsIndeterminate = true,
                Text = Message
            };
            SystemTray.SetProgressIndicator(Deployment.Current, _progress);
        }
    }
}