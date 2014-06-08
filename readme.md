ACR Xamarin Forms
=================

##Camera & Gallery
#Powered By
Xamarin.Mobile

##Address Book Management
#Powered By
Xamarin.Mobile

##Location Services
#Powered By
Xamarin.Mobile

##Email


##Phone & SMS


##User Dialogs
Allows for messagebox style dialogs

* Action Sheet (multiple choice menu)
* Alert
* Prompt
* Confirm
* Loading
* Progress
* Toast

#Powered By:
* Android - Progress/Loading uses AndHUD
* iOS - Progress/Loading uses BTProgressHUD
* WinPhone - All dialogs by Coding4Fun Toolkit  


##Bar Code Scanner
Powered by Redth's ZXing.Net.Mobile

    new Command(async () => {
        var scan = DependencyService.Get<IBarCodeScanner>();
        var r = await scan.Read(flashlightText: "Turn on flashlight", cancelText: "Cancel");

        Result = (r.Success 
            ? String.Format("Barcode Result - Format: {0} - Code: {1}", r.Format, r.Code)
            : "Cancelled barcode scan"
        );
    });


##Network
Detecting network state changes so that you can inform
the user when they were working in an offline state.

* INetworkService subscribes to INotifyPropertyChanged and monitors the device network status
* INetworkService.NetworkStatusChanged event for background processes to monitor


##Settings
A simple settings library that works differently than the traditional setting plugins out there.  Instead, my approach was to work
with an observable dictionary.


##Device Info
Allows you to get the information of the device for auditing purposes

* Device Manufacturer
* Operating System and Version
* Front and rear facing cameras
* Screen Resolution