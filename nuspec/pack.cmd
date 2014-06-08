@echo off
del *.nupkg
nuget pack Acr.XamForms.nuspec
nuget pack Acr.XamForms.Mobile.nuspec
nuget pack Acr.XamForms.BarCodeScanner.nuspec
nuget pack Acr.XamForms.DeviceInfo.nuspec
nuget pack Acr.XamForms.Settings.nuspec
nuget pack Acr.XamForms.UserDialogs.nuspec
nuget pack Acr.XamForms.Network.nuspec
nuget pack Acr.XamForms.SignaturePad.nuspec
pause