using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace Acr.XamForms.Mobile.iOS {
    
    public class PhoneService : IPhoneService {
        
        #region IPhoneService Members

        public void Call(string person, string number) {
            var url = new NSUrl("tel:" + number);
            UIApplication.SharedApplication.OpenUrl(url);
        }


        public void Sms(string number, string message) {
            var smsTo = NSUrl.FromString("sms:" + number);
            if (UIApplication.SharedApplication.CanOpenUrl(smsTo)) {
                UIApplication.SharedApplication.OpenUrl(smsTo);
            } else {
                // warn the user, or hide the button...
            }
        }

        #endregion

/*
         private readonly IMvxTouchModalHost _modalHost;
        private MFMessageComposeViewController _sms;

        public SmsTask()
        {
            _modalHost = Mvx.Resolve<IMvxTouchModalHost>();
        }

        public void SendSMS(string body, string phoneNumber)
        {
            if (!MFMessageComposeViewController.CanSendText)
                return;

            _sms = new MFMessageComposeViewController {Body = body, Recipients = new[] {phoneNumber}};
            _sms.Finished += HandleSmsFinished;

            _modalHost.PresentModalViewController(_sms, true);
        }

        private void HandleSmsFinished(object sender, MFMessageComposeResultEventArgs e)
        {
            var uiViewController = sender as UIViewController;
            if (uiViewController == null)
                throw new ArgumentException("sender");

            uiViewController.DismissViewController(true, () => {});
            _modalHost.NativeModalViewControllerDisappearedOnItsOwn();
        }
 */
    }
}