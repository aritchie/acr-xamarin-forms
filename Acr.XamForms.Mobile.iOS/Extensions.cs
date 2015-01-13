using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;


namespace Acr.XamForms.Mobile.iOS {

    public static class Extensions {

        public static IDictionary<string, string> AsDictionary(this NSUserDefaults defaults) {
            return defaults
                .ToDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
        }
    }
}