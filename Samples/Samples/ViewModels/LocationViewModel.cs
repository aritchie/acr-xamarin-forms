using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class LocationViewModel : ViewModel {
        private readonly ILocationService location;
        private readonly IUserDialogService dialogs;
        private readonly ILogger log;
        private bool isRefreshing;


        public LocationViewModel(ILocationService location, IUserDialogService dialogs, ILogger log) {
            this.location = location;
            this.dialogs = dialogs;
            this.log = log;
        }


        private async Task RefreshPosition() {
            try { 
                this.isRefreshing = true;
                //this.location.StartListening();
                var pos = await this.location.GetPositionAsync();
                this.Latitude = pos.Latitude;
                this.Longitude = pos.Longitude;
                this.Altitude = pos.Altitude;
                this.Heading = pos.Heading;
            }
            catch (Exception ex) {
                this.log.Error("ERROR", ex);
                this.dialogs.Alert("There was an error retrieving your position");
            }
            finally {
                this.isRefreshing = false;
            }
        }

        
        private double latitude;
        public double Latitude {
            get { return this.latitude; }
            private set { this.SetProperty(ref this.latitude, value); }
        }


        private double longitude;
        public double Longitude {
            get { return this.longitude; }
            private set { this.SetProperty(ref this.longitude, value); }
        }


        private double altitude;
        public double Altitude {
            get { return this.altitude; }
            private set { this.SetProperty(ref this.altitude, value); }
        }


        private double heading;
        public double Heading {
            get { return this.heading; }
            private set { this.SetProperty(ref this.heading, value); }
        }


        private ICommand refreshCommand;
        public ICommand Refresh {
            get {
                this.refreshCommand = this.refreshCommand = new Command(() => this.RefreshPosition(), () => this.isRefreshing);
                return this.refreshCommand;
            }
        }
    }
}