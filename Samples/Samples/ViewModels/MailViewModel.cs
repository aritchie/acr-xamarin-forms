using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class MailViewModel : ViewModel {

        public MailViewModel(IMailService mailer) {
            this.Send = new Command(() => {});
        }


        private string to;
        public string To {
            get { return this.to; }
            set { this.SetProperty(ref this.to, value); }
        }


        public ICommand Send { get; private set; }
    }
}
