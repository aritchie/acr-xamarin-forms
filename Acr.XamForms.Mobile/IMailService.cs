using System;


namespace Acr.XamForms.Mobile {

    public interface IMailService {

        void Send(string from, string to, string subject, string message);
        void Send(MailMessage mail);
    }
}