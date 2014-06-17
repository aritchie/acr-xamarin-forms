using System;
using MonoTouch.Foundation;


namespace Acr.XamForms.Mobile.iOS {
    
    [Preserve]
    public class MailService : IMailService {
// TODO
        public void Send(string from, string to, string subject, string message) {
            throw new NotImplementedException();
        }


        public void Send(MailMessage mail) {
            throw new NotImplementedException();
        }
    }
}