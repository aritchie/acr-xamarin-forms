using System;


namespace Acr.XamForms {
    
    public static class Extensions {

        public static bool IsEmpty(this string @string) {
            return String.IsNullOrWhiteSpace(@string);
        }
    }
}