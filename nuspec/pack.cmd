@echo off
del *.nupkg
nuget pack Acr.XamForms.nuspec
nuget pack Acr.XamForms.Mobile.nuspec
nuget pack Acr.XamForms.BarCodeScanner.nuspec
nuget pack Acr.XamForms.UserDialogs.nuspec
nuget pack Acr.XamForms.SignaturePad.nuspec
pause