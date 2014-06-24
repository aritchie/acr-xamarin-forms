//using System;


//namespace Acr.XamForms.Mobile {
    
//    public abstract class AbstractMailService : IMailService {

//        public void Send(string to, string subject, string message, bool isHtml) {
//            this.Send(new MailMessage {
//                Subject = subject,
//                Message = message,
//                IsHtml = isHtml,
//                To = new[] {
//                    new MailAddress(to), 
//                }
//            });
//        }


//        public abstract void Send(MailMessage mail);


//        public virtual bool SupportsAttachments {
//            get { return true; }
//        }
//    }
//}
