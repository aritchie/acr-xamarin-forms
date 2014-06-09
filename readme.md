ACR Xamarin Forms
=================

##Camera & Gallery
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


##Text-To-Speech


##File Viewer


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


##Signature Pad
Call for a signature pad dialog in 1 line of xplat code from a view model!

    var signatureService = DepedencyService.Get<ISignatureService>();
    
    signatureService.RequestSignature(result => {
        if (result.Cancelled)
            return;

        // use the image stream to write to file or serialize the draw points
        // result.Stream or result.Points
    });


    signatureService.LoadSignature(drawPoints);


#Configuration

    signatureService.DefaultConfiguration.ClearText = "Why clear?";

    or pass overridden configuration to each method:

    signatureService.RequestSignature(callback, new SignaturePadConfiguration {
        SaveText = "Signed!",
        CancelText = "No way!",
        PromptText = "Right here"
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