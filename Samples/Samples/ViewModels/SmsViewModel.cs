using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class SmsViewModel : ViewModel {

        public SmsViewModel(IPhoneService phone, IUserDialogService dialogs) {
            this.Send = new Command(() => {
                phone.Sms(this.PhoneNumber, this.Message);
                dialogs.Alert("Message sent");
            });
        }


        public ICommand Send { get; private set; }


        private int count;
        public int Count {
            get { return this.count; }
            set { this.SetProperty(ref this.count, value); }
        }


        private string phoneNumber;
        public string PhoneNumber {
            get { return this.phoneNumber; }
            set {
                if (value == null)
                    return;

                if (char.IsDigit(value, value.Length - 1) && value.Length <= 10)
                    this.phoneNumber = value;

                this.OnPropertyChanged();
            }
        }


        private string message = "";
        public string Message {
            get { return this.message; }
            set {
                if (value == null)
                    return;

                if (this.message.Length <= 140)
                    this.message = value;

                this.Count = this.message.Length;
                this.OnPropertyChanged();
                    
            }
        }
    }
}
