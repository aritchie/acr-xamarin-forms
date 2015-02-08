using System;

namespace Acr.XamForms.UserDialogs
{
    public interface IDownloadDialog : IDisposable
    {
        string Message { get; set; }
        bool IsShowing { get; }

        void Show();
        void Hide();
    }
}