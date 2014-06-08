using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class SmsViewModel : ViewModel {

        public SmsViewModel(IPhoneService phone, IUserDialogService dialogs) {
            this.Send = new Command(() => {
                if (this.Count > 140)
                    dialogs.Alert("Text message is too big");
                else {
                    phone.Sms(this.PhoneNumber, this.Message);
                    dialogs.Alert("Message sent");
                }
            });
        }


        public ICommand Send { get; private set; }


        private int count;
        public int Count {
            get { return this.count; }
            private set { this.SetProperty(ref this.count, value); }
        }


        private string phoneNumber;
        public string PhoneNumber {
            get { return this.phoneNumber; }
            set { this.SetProperty(ref this.phoneNumber, value); }
        }


        private string message;
        public string Message {
            get { return this.message; }
            set {
                if (this.SetProperty(ref this.message, value))
                    this.Count = this.message.Length;
            }
        }
    }
}
