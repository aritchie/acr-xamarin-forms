using System;

namespace Acr.XamForms.Mobile {

    public interface ISettings {

        ISettingsDictionary All { get; } 

        string Get(string key, string defaultValue = null);
        void Set(string key, string value);
        void Remove(string key);
        void Clear();
        void Resync();
        bool Contains(string key);
    }
}
