using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;


namespace Acr.XamForms.Mobile {
    
    public abstract class AbstractSettings : ISettings {

        protected abstract IDictionary<string, string> GetNativeSettings();
        protected abstract void AddOrUpdateNative(IEnumerable<KeyValuePair<string, string>> saves);
        protected abstract void RemoveNative(IEnumerable<KeyValuePair<string, string>> saves);
        protected abstract void ClearNative();

        #region Internals


        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Replace:
                    var saves = e.NewItems.Cast<KeyValuePair<string, string>>();
                    this.AddOrUpdateNative(saves);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var dels = e.OldItems.Cast<KeyValuePair<string, string>>();
                    this.RemoveNative(dels);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.ClearNative();
                    break;
            }
        }

        #endregion

        #region ISettings Members

        private ISettingsDictionary all;
        public ISettingsDictionary All {
            get {
                if (this.all == null)
                    this.Resync();

                return this.all;
            }
            private set { this.all = value; }
        }


        /// <summary>
        /// This resynchronizes the settings from the native settings dictionary
        /// </summary>
        /// <param name="dictionary"></param>
        /// 
        public void Resync() {
            if (this.All != null) {
                this.All.CollectionChanged -= this.OnCollectionChanged;
                this.All = null;
            }
            var settings = this.GetNativeSettings();
            this.All = new SettingsDictionary(settings);
            this.All.CollectionChanged += this.OnCollectionChanged;
           
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
