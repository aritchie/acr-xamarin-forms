using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Acr.XamForms.UserDialogs.iOS
{
    public class DownloadDialog : IDownloadDialog
    {
        public DownloadDialog()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
        }

        public void Dispose()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
        }

        public string Message { get; set; }

        public bool IsShowing
        {
            get { return UIApplication.SharedApplication.NetworkActivityIndicatorVisible; }
        }

        public void Show()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
        }

        public void Hide()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
        }
    }
}