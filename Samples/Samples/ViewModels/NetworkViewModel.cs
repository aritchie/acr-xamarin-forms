using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class NetworkViewModel : ViewModel {

        public INetworkService Network { get; private set; }


        public NetworkViewModel(INetworkService networkService, IUserDialogService dialogService) {
            this.Host = "google.ca";

            this.Network = networkService;
            this.Ping = new Command(async () => {
                if (String.IsNullOrWhiteSpace(this.Host)) {
                    dialogService.Alert("You must enter a host");
                }
                else {
                    var reached = false;
                    using (dialogService.Loading()) {
                        reached = await networkService.IsHostReachable(this.Host);
                    }
                    var msg = (reached
                        ? " is reachable"
                        : " cannot be reached"
                    );
                    dialogService.Alert(this.Host + msg);
                }
            });
        }


        public ICommand Ping { get; private set; }


        private string host;
        public string Host {
            get { return this.host; }
            set { this.SetProperty(ref this.host, value); }
        }
    }
}
