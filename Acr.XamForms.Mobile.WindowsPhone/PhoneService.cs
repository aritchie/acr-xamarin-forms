using System;
using Microsoft.Phone.Tasks;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class PhoneService : IPhoneService {

        #region IPhoneService Members

        public void Call(string name, string number) {
            var task = new PhoneCallTask {
                DisplayName = name,
                PhoneNumber = number
            };
            task.Show();
        }


        public void Sms(string number, string message) {
            var task = new SmsComposeTask {
                To = number,
                Body = message
            };
            task.Show();
        }

        #endregion
    }
}
