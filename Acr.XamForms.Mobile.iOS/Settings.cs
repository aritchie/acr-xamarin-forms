using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;


namespace Acr.XamForms.Mobile.iOS {
    
    public class Settings : AbstractSettings {
        private static readonly NSUserDefaults prefs = NSUserDefaults.StandardUserDefaults;


        protected override IDictionary<string, string> GetNativeSettings() {
            return prefs
                .AsDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
        }


        protected override void AddOrUpdateNative(IEnumerable<KeyValuePair<string, string>> saves) {
            foreach (var item in saves)
                prefs.SetString(item.Key, item.Value);

            prefs.Synchronize();
        }


        protected override void RemoveNative(IEnumerable<KeyValuePair<string, string>> dels) {
            foreach (var item in dels)
                prefs.RemoveObject(item.Key);

            prefs.Synchronize();
        }

        protected override void ClearNative() {
            prefs.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
            prefs.Synchronize();
        }
    }
}