using System;
using Xamarin.Forms;
using Acr.XamForms.Mobile;
#if __ANDROID__
using Acr.XamForms.Mobile.Droid;
#elif __IOS__
using Acr.XamForms.Mobile.iOS;
#elif WINDOWS_PHONE
using Acr.XamForms.Mobile.WindowsPhone;
#endif

[assembly: Dependency(typeof(DeviceInfo))]
[assembly: Dependency(typeof(FileViewer))]
[assembly: Dependency(typeof(LocationService))]
[assembly: Dependency(typeof(MailService))]
[assembly: Dependency(typeof(NetworkService))]
[assembly: Dependency(typeof(PhoneService))]
[assembly: Dependency(typeof(PhotoService))]
[assembly: Dependency(typeof(Settings))]
[assembly: Dependency(typeof(TextToSpeechService))]