using System;


namespace Acr.XamForms.Mobile {
    
    public static class MailExtensions {

        public static MailMessage AddTo(this MailMessage mail, string address) {
            mail.To.Add(new MailAddress(address));
            return mail;
        }


        public static MailMessage AddCc(this MailMessage mail, string address) {
            mail.Cc.Add(new MailAddress(address));
            return mail;
        }


        public static MailMessage AddBcc(this MailMessage mail, string address) {
            mail.Bcc.Add(new MailAddress(address));
            return mail;
        }
    }
}
