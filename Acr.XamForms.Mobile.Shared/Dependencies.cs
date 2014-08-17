using System;
using Xamarin.Forms;
using Acr.XamForms.Mobile.Locations;
using Acr.XamForms.Mobile.Media;
#if __ANDROID__
using Acr.XamForms.Mobile.Droid;
#elif __IOS__
using Acr.XamForms.Mobile.iOS;
#elif WINDOWS_PHONE
using Acr.XamForms.Mobile.WindowsPhone;
#endif

[assembly: Dependency(typeof(DeviceInfo))]
[assembly: Dependency(typeof(FileViewer))]
[assembly: Dependency(typeof(GeoLocator))]
//[assembly: Dependency(typeof(MailService))]
[assembly: Dependency(typeof(PhoneService))]
[assembly: Dependency(typeof(MediaPicker))]
[assembly: Dependency(typeof(Settings))]
[assembly: Dependency(typeof(TextToSpeechService))]