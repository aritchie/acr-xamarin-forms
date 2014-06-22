using System;
using System.Windows.Input;
using Acr.XamForms.Mobile;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {
    
    public class LocationViewModel : ViewModel {
        private readonly ILocationService location;


        public LocationViewModel(ILocationService location) {
            this.position = new Position();
            this.location = location;
        }


        private Position position;
        public Position Position {
            get { return this.position; }
            private set {
                this.position = value;
                this.OnPropertyChanged();
            }
        }


        private ICommand refreshCommand;
        public ICommand Refresh {
            get {
                this.refreshCommand = this.refreshCommand = new Command(async () => 
                    this.Position = await this.location.GetPositionAsync()
                );
                return this.refreshCommand;
            }
        }
    }
}
