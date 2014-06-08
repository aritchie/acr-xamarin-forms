@echo off
del *.nupkg
nuget pack Acr.Xamarin.Forms.nuspec
nuget pack Acr.Xamarin.Forms.BarCodeScanner.nuspec
nuget pack Acr.Xamarin.Forms.DeviceInfo.nuspec
nuget pack Acr.Xamarin.Forms.Settings.nuspec
nuget pack Acr.Xamarin.Forms.UserDialogs.nuspec
nuget pack Acr.Xamarin.Forms.Network.nuspec
pause