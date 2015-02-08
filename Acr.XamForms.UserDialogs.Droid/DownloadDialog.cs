using System.Threading.Tasks;
using Android.App;

namespace Acr.XamForms.UserDialogs.Droid
{
    public class DownloadDialog : IDownloadDialog
    {
        public DownloadDialog()
        {
            Utils.RequestMainThread(() =>
            {
                var activity = Utils.GetActivityContext() as Activity;
                if (activity == null)
                    return;
                IsShowing = true;
                activity.SetProgressBarIndeterminateVisibility(true);
            });
        }

        public void Dispose()
        {
            Utils.RequestMainThread(() =>
            {
                var activity = Utils.GetActivityContext() as Activity;
                if (activity == null)
                    return;
                IsShowing = false;
                activity.SetProgressBarIndeterminateVisibility(false);
            });
        }

        public string Message { get; set; }

        public bool IsShowing { get; private set; }

        public void Show()
        {
            Utils.RequestMainThread(() =>
            {
                var activity = Utils.GetActivityContext() as Activity;
                if (activity == null)
                    return;
                IsShowing = true;
                activity.SetProgressBarIndeterminateVisibility(true);
            });
        }

        public void Hide()
        {
            Utils.RequestMainThread(() =>
            {
                var activity = Utils.GetActivityContext() as Activity;
                if (activity == null)
                    return;
                IsShowing = false;
                activity.SetProgressBarIndeterminateVisibility(false);
            });
        }
    }
}