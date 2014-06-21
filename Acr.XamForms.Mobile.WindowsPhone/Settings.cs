using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;


namespace Acr.XamForms.Mobile.WindowsPhone {
    
    public class Settings : AbstractSettings {
        private static readonly IsolatedStorageSettings prefs = IsolatedStorageSettings.ApplicationSettings;

        protected override IDictionary<string, string> GetNativeSettings() {
            return prefs.ToDictionary(x => x.Key, x => x.Value.ToString());
        }


        protected override void AddOrUpdateNative(IEnumerable<KeyValuePair<string, string>> saves) {
            foreach (var item in saves)
                prefs[item.Key] = item.Value;

            prefs.Save();
        }


        protected override void RemoveNative(IEnumerable<KeyValuePair<string, string>> dels) {
            foreach (var item in dels)
                prefs.Remove(item.Key);

            prefs.Save();
        }

        protected override void ClearNative() {
            prefs.Clear();
            prefs.Save();
        }
    }
}