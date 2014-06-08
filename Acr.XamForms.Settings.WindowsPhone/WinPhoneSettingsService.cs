using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using Acr.XamForms.Settings.WindowsPhone;
using Xamarin.Forms;


[assembly: Dependency(typeof(WinPhoneSettingsService))]


namespace Acr.XamForms.Settings.WindowsPhone {
    
    public class WinPhoneSettingsService : AbstractSettingsService {
       
        protected IsolatedStorageSettings Set {
            get { return IsolatedStorageSettings.ApplicationSettings; }
        }


        protected override IDictionary<string, string> GetNativeSettings() {
            return this.Set.ToDictionary(x => x.Key, x => x.Value.ToString());;
        }


        protected override void SaveSetting(string key, string value) {
            this.Set[key] = value;
            this.Set.Save();
        }


        protected override void RemoveSetting(string key) {
            this.Set.Remove(key);
            this.Set.Save();
        }


        protected override void ClearSettings() {
            this.Set.Clear();
            this.Set.Save();
        }
    }
}
