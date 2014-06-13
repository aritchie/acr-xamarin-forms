using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Acr.XamForms.Settings;
using Xamarin.Forms;
#if __ANDROID__
using Android.Content;
using Android.Preferences;
#elif __IOS__
using MonoTouch.Foundation;
#elif WINDOWS_PHONE
using System.IO.IsolatedStorage;
#endif


[assembly: Dependency(typeof(Settings))]


namespace Acr.XamForms.Settings {
    
    public class Settings : ISettings {

        public Settings() {
            this.Resync();
        }

        #region Internals
#if __ANDROID__
        private static readonly ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Forms.Context.ApplicationContext);
#elif __IOS__
        private static readonly NSUserDefaults prefs = NSUserDefaults.StandardUserDefaults;
#elif WINDOWS_PHONE
        private static readonly IsolatedStorageSettings prefs = IsolatedStorageSettings.ApplicationSettings;
#endif

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Replace:
                    var saves = e.NewItems.Cast<KeyValuePair<string, string>>();
#if __ANDROID__
                    using (var editor = prefs.Edit()) {
                        foreach (var item in saves)
                            editor.PutString(item.Key, item.Value);
                        
                        editor.Commit();
                    }
#elif __IOS__
                    foreach (var item in saves)
                        prefs.SetString(item.Key, item.Value);

                    prefs.Synchronize();
#elif WINDOWS_PHONE
                    foreach (var item in saves)
                        prefs[item.Key] = item.Value;

                    prefs.Save();
#endif
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var dels = e.OldItems.Cast<KeyValuePair<string, string>>();
#if __ANDROID__
                    using (var editor = prefs.Edit()) {
                        foreach (var item in dels) 
                            editor.Remove(item.Key);

                        editor.Commit();
                    }
#elif __IOS__
                    foreach (var item in dels)
                        prefs.RemoveObject(item.Key);

                    prefs.Synchronize();
#elif WINDOWS_PHONE
                    foreach (var item in dels)
                        prefs.Remove(item.Key);

                    prefs.Save();
#endif
                    break;

                case NotifyCollectionChangedAction.Reset:
#if __ANDROID__
                    using (var editor = prefs.Edit()) {
                        editor.Clear();
                        editor.Commit();
                    }
#elif __IOS__
                    prefs.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
                    prefs.Synchronize();
#elif WINDOWS_PHONE
                    prefs.Clear();
                    prefs.Save();
#endif
                    break;
            }
        }

        #endregion

        #region ISettings Members

        public IDictionary<string, string> All { get; private set; }


        /// <summary>
        /// This resynchronizes the settings from the native settings dictionary
        /// </summary>
        /// <param name="dictionary"></param>
        /// 
        public void Resync() {
#if __ANDROID__
            var settings = prefs.All.ToDictionary(y => y.Key, y => y.Value.ToString());
#elif __IOS__
            var settings = prefs
                .AsDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
#elif WINDOWS_PHONE
            var settings = prefs.ToDictionary(x => x.Key, x => x.Value.ToString());
#endif
            if (this.All == null) {
                 var observable = new ObservableDictionary<string, string>(settings);
                observable.CollectionChanged += this.OnCollectionChanged;
                this.All = observable;
            }
            else {
                this.All.Clear();
                foreach (var set in settings)
                    this.All.Add(set);
            }
        }


        public virtual string Get(string key, string defaultValue = null) {
            return (this.All.ContainsKey(key)
                ? this.All[key]
                : defaultValue
            );
        }


        public virtual void Set(string key, string value) {
            this.All[key] = value;
        }


        public virtual void Remove(string key) {
            if (this.All.ContainsKey(key))
                this.All.Remove(key);
        }


        public virtual void Clear() {
            this.All.Clear();
        }


        public virtual bool Contains(string key) {
            return this.All.ContainsKey(key);
        }

        #endregion
    }
}
