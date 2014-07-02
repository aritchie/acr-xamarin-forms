using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;


namespace Acr.XamForms.Mobile.iOS {
    
    public class Settings : AbstractSettings {
        private static readonly NSUserDefaults prefs = NSUserDefaults.StandardUserDefaults;
        public static readonly string[] ProtectedSettingsKeys = new [] {
            "WebKitKerningAndLigaturesEnabledByDefault",
            "AppleLanguages",
            "monodevelop-port",
            "AppleITunesStoreItemKinds",
            "AppleLocale",
            "connection-mode",
            "AppleKeyboards",
            "NSLanguages",
            "UIDisableLegacyTextView",
            "NSInterfaceStyle"
        };


        protected override IDictionary<string, string> GetNativeSettings() {
            return prefs
                .AsDictionary()
                .Where(x => this.CanTouch(x.Key.ToString()))
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
        }


        protected override void AddOrUpdateNative(IEnumerable<KeyValuePair<string, string>> saves) {
            foreach (var item in saves)
                if (this.CanTouch(item.Key))
                    prefs.SetString(item.Key, item.Value);

            prefs.Synchronize();
        }


        protected override void RemoveNative(IEnumerable<KeyValuePair<string, string>> dels) {
            foreach (var item in dels)
                if (this.CanTouch(item.Key))
                    prefs.RemoveObject(item.Key);

            prefs.Synchronize();
        }


        protected override void ClearNative() {
            foreach (var item in this.All)
                if (this.CanTouch(item.Key))
                    prefs.RemoveObject(item.Key);
                    
            //prefs.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
            prefs.Synchronize();
        }


        protected virtual bool CanTouch(string settingsKey) {
            return !ProtectedSettingsKeys.Any(x => x.Equals(settingsKey, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}