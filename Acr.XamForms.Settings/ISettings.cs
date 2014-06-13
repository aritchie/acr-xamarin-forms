using System;
using System.Collections.Generic;


namespace Acr.XamForms.Settings {

    public interface ISettings {

        IDictionary<string, string> All { get; } 

        string Get(string key, string defaultValue = null);
        void Set(string key, string value);
        void Remove(string key);
        void Clear();
        void Resync();
        bool Contains(string key);
    }
}
