using System;
using Acr.XamForms.Mobile;

#if __IOS__
using Acr.XamForms.Mobile.iOS;
#elif WINDOWS_PHONE
using Acr.XamForms.Mobile.WindowsPhone;
#else
using Acr.XamForms.Mobile.Droid;
#endif
using Xamarin.Forms;


[assembly: Dependency(typeof(DeviceInfo))]
[assembly: Dependency(typeof(LocationService))]
[assembly: Dependency(typeof(FileViewer))]
[assembly: Dependency(typeof(MailService))]
[assembly: Dependency(typeof(PhoneService))]
[assembly: Dependency(typeof(PhotoService))]
[assembly: Dependency(typeof(TextToSpeechService))]