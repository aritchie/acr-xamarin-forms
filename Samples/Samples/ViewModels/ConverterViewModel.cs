using System;
using Acr.XamForms.ViewModels;


namespace Samples.ViewModels {
    
    public class ConverterViewModel : ViewModel {

        private string phoneNumber;
        public string PhoneNumber {
            get { return this.phoneNumber; }
            set { this.SetProperty(ref this.phoneNumber, value); }
        }


        private string creditCardNumber;
        public string CreditCardNumber {
            get { return this.creditCardNumber; }
            set { this.SetProperty(ref this.creditCardNumber, value); }
        }

        // FILE SIZE
        public long Bytes {
            get { return 1; }
        }


        public long KB {
            get { return Bytes * 1024; }
        }


        public long MB {
            get { return KB * 1024; }
        }


        public long GB {
            get { return MB * 102; }
        }


        // TIME AGO
        // TODO: what about future tenses?
        public DateTime Now {
            get { return DateTime.Now; }
        }


        public DateTime HoursAgo {
            get { return DateTime.Now.AddHours(-8); }
        }


        public DateTime DaysAgo {
            get { return DateTime.Now.AddDays(-3); }
        }


        public DateTime WeeksAgo {
            get { return DateTime.Now.AddDays(-28); }            
        }
    }
}
