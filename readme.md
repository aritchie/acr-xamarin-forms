ACR Xamarin Forms
=================

##User Dialogs
Allows for messagebox style dialogs to be called from your view model commands

* Action Sheet (multiple choice menu)
* Alert
* Prompt
* Confirm
* Loading
* Progress
* Toast

[examples](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/UserDialogViewModel.cs)

* Android - Progress/Loading uses Redth's [AndHUD](https://github.com/Redth/AndHUD)
* iOS - Progress/Loading uses Nic Wise's [BTProgressHUD](https://github.com/nicwise/BTProgressHUD)
* WinPhone - All dialogs by [Coding4Fun Toolkit](http://coding4fun.codeplex.com/) 


##Signature Pad
Control provided by [Xamarin Signature Pad](https://github.com/xamarin/SignaturePad).  This library provides a way to call for a dialog from a view model command or
it can be used within your XAML views.

* [XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/SignatureXamlView.xaml)
* [ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/SignatureListViewModel.cs)


##Camera & Gallery
Camera and Photo Gallery access powered by [Xamarin.Mobile](https://github.com/xamarin/Xamarin.Mobile)

* [Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/PhotoViewModel.cs)


##Bar Code Scanner
Powered by Redth's awesome [ZXing.Net.Mobile](https://github.com/Redth/ZXing.Net.Mobile).  This library provides a way to call for a barcode dialog from your view model command

* [Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/BarCodeViewModel.cs)


##Location Services
Geo-Locator services powered by [Xamarin.Mobile](https://github.com/xamarin/Xamarin.Mobile)

* [ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/LocationViewModel.cs)
* [View-XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/LocationView.xaml)


##File Management
Allows for directory/file manipulation using the standard FileInfo/DirectoryInfo scheme

[Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/SignatureListViewModel.cs)


##Network
Detecting network state changes so that you can inform
the user when they were working in an offline state.

* INetworkService subscribes to INotifyPropertyChanged and monitors the device network status
* INetworkService.NetworkStatusChanged event for background processes to monitor

* [ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/NetworkViewModel.cs)
* [View-XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/NetworkView.xaml)


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

* [ViewModel Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/ViewModels/DeviceInfoViewModel.cs)
* [View-XAML Example](https://github.com/aritchie/acr-xamarin-forms/blob/master/Samples/Samples/Views/DeviceInfoView.xaml)
