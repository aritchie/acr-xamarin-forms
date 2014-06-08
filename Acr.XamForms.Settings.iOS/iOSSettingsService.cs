using System;
using System.Collections.Generic;
using System.Linq;
using Acr.XamForms.Settings.iOS;
using MonoTouch.Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSSettingsService))]


namespace Acr.XamForms.Settings.iOS {

    public class iOSSettingsService : AbstractSettingsService {

        protected NSUserDefaults Cfg {
            get { return NSUserDefaults.StandardUserDefaults; }
        }


        protected override IDictionary<string, string> GetNativeSettings() {
            return this.Cfg
                .AsDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
        }


        protected override void SaveSetting(string key, string value) {
            this.Cfg.SetString(value, key);
            this.Cfg.Synchronize();
        }


        protected override void ClearSettings() {
            this.Cfg.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
            this.Cfg.Synchronize();
        }


        protected override void RemoveSetting(string key) {
            this.Cfg.RemoveObject(key);
            this.Cfg.Synchronize();
        }
    }
}
