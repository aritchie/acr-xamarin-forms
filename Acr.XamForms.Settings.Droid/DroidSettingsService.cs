using System;
using System.Collections.Generic;
using System.Linq;
using Acr.XamForms.Settings.Droid;
using Android.Content;
using Android.Preferences;
using Xamarin.Forms;


[assembly: Dependency(typeof(DroidSettingsService))]


namespace Acr.XamForms.Settings.Droid {

    public class DroidSettingsService : AbstractSettingsService {
        private ISharedPreferences prefs;
        

        protected override IDictionary<string, string> GetNativeSettings() {
            this.prefs = PreferenceManager.GetDefaultSharedPreferences(Forms.Context.ApplicationContext);
            return this.prefs.All.ToDictionary(y => y.Key, y => y.Value.ToString());
        }


        protected override void SaveSetting(string key, string value) {
            using (var editor = this.prefs.Edit()) {
                editor.PutString(key, value);
                editor.Commit();
            }
        }


        protected override void RemoveSetting(string key) {
            using (var editor = this.prefs.Edit()) {
                editor.Remove(key);
                editor.Commit();
            }            
        }


        protected override void ClearSettings() {
            using (var editor = this.prefs.Edit()) {
                editor.Clear();
                editor.Commit();
            }
        }
    }
}
