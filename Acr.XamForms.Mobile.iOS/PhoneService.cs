using System;
using Foundation;
using UIKit;


namespace Acr.XamForms.Mobile.iOS {

    [Preserve]
    public class PhoneService : IPhoneService {

        public void Call(string person, string number) {
            var url = new NSUrl("tel:" + number);
            UIApplication.SharedApplication.OpenUrl(url);
        }


        public void Sms(string number, string message) {
            // TODO: pass message
            var url = NSUrl.FromString("sms:" + number);
            UIApplication.SharedApplication.OpenUrl(url);
        }
    }
}