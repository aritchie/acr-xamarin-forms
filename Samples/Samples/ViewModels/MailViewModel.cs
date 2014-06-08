using System;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class MailViewModel : ViewModel {
        private readonly IMailService mailer;


        public MailViewModel(IMailService mailer) {
            this.mailer = mailer;
        }
    }
}
