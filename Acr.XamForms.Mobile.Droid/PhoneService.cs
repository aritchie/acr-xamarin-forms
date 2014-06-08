using Android.Net;
using Android.Content;
using Android.Telephony;
using Xamarin.Forms;


namespace Acr.XamForms.Mobile.Droid {
    
    public class PhoneService : IPhoneService {

        #region IPhoneService Members

        public void Call(string person, string number) {
            var uri =  Uri.Parse("tel:" + number);
            var intent = new Intent(Intent.ActionDial, uri);
            Forms.Context.StartActivity(intent);
        }


        public void Sms(string number, string message) {
            SmsManager.Default.SendTextMessage(number, null, message, null, null);
        }

        #endregion
    }
}