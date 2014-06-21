using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Acr.XamForms.Mobile {
    
    internal class SettingsDictionary : ObservableDictionary<string, string>, ISettingsDictionary {

        internal SettingsDictionary(IDictionary<string, string> current) : base(current) { }
    }
}
