using System;
using System.Linq;
using System.Windows.Input;
using Acr.XamForms;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class MailViewModel : ViewModel {

        public MailViewModel(IMailService mailer) {
            this.Send = new Command(() => {
                var mail = new MailMessage {
                    Subject = this.Subject,
                    Message = this.Message
                };
                this.To
                    .Split(';')
                    .Select(x => x.Trim())
                    .ToList()
                    .ForEach(x => mail.AddTo(x));

                mailer.Send(mail);
            });
        }


        private string to;
        public string To {
            get { return this.to; }
            set { this.SetProperty(ref this.to, value); }
        }


        private string subject;
        public string Subject {
            get { return this.subject; }
            set { this.SetProperty(ref this.subject, value); }
        }


        private string message;
        public string Message {
            get { return this.message; }
            set { this.SetProperty(ref this.message, value); }
        }


        public ICommand Send { get; private set; }
    }
}
