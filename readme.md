ACR Xamarin Forms
=================

##User Dialogs
Allows for messagebox style dialogs

* Action Sheet (multiple choice menu)
* Alert
* Prompt
* Confirm
* Loading
* Progress
* Toast

[examples](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/UserDialogViewModel.cs)

* Android - Progress/Loading uses AndHUD
* iOS - Progress/Loading uses BTProgressHUD
* WinPhone - All dialogs by Coding4Fun Toolkit  


##Camera & Gallery
Camera and Photo Gallery access powered by Xamarin.Mobile

[Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/PhotoViewModel.cs)


##Bar Code Scanner
Allows for quick barcode scanning from a viewmodel command

[Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/BarCodeViewModel.cs)


##Location Services
Geo-Locator services powered by Xamarin.Mobile

[ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/LocationViewModel.cs)
[View-XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/LocationView.xaml)


##Network
Detecting network state changes so that you can inform
the user when they were working in an offline state.

* INetworkService subscribes to INotifyPropertyChanged and monitors the device network status
* INetworkService.NetworkStatusChanged event for background processes to monitor

[ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/NetworkViewModel.cs)
[View-XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/NetworkView.xaml)

##Settings
A simple settings library that works differently than the traditional setting plugins out there.  Instead, my approach was to work
with an observable dictionary.

[Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/SettingsViewModel.cs)


##Device Info
Allows you to get the information of the device for auditing purposes

* Device Manufacturer
* Operating System and Version
* Front and rear facing cameras
* Screen Resolution

[ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/DeviceInfoViewModel.cs)
[View-XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/DeviceInfoView.xaml)
