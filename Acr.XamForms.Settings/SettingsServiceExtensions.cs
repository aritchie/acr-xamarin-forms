using System;
using Newtonsoft.Json;


namespace Acr.XamForms.Settings {
    
    public static class SettingsServiceExtensions {

        public static void SetObject(this ISettingsService settings, string key, object obj) {
            var value = JsonConvert.SerializeObject(obj);
            settings.Set(key, value);
        }


        public static T GetObject<T>(this ISettingsService settings, string key) where T : class {
            var value = settings.Get(key);
            if (String.IsNullOrWhiteSpace(value))
                return null;

            return JsonConvert.DeserializeObject<T>(value);
        }


        public static void SetIfNotSet(this ISettingsService settings, string key, object obj) {
            if (!settings.Contains(key))
                settings.Set(key, JsonConvert.SerializeObject(obj));
        }


        public static void SetIfNotSet(this ISettingsService settings, string key, string value) {
            if (settings.Contains(key))
                settings.Set(key, value);
        }


        public static int GetInt(this ISettingsService settings, string key, int defaultValue = 0) {
            var value = settings.Get(key);
            var r = defaultValue;
            if (!Int32.TryParse(value, out r))
                return defaultValue;

            return r;
        }


        public static long GetLong(this ISettingsService settings, string key, long defaultValue = 0) {
            return Int64.Parse(settings.Get(key, defaultValue.ToString()));
        }


        public static void SetDateTime(this ISettingsService settings, string key, DateTime dateTime) {
            settings.Set(key, dateTime.ToString());
        }


        public static DateTime? GetDateTime(this ISettingsService settings, string key, DateTime? defaultValue = null) {
            var s = settings.Get(key);
            if (s == null)
                return defaultValue;

            return DateTime.Parse(s);
        }


        public static void SetDateTimeOffset(this ISettingsService settings, string key, DateTimeOffset dateTime) {
            settings.Set(key, dateTime.ToString());
        }


        public static DateTimeOffset? GetDateTimeOffset(this ISettingsService settings, string key, DateTimeOffset? defaultValue = null) {
            var s = settings.Get(key);
            if (s == null)
                return defaultValue;

            return DateTimeOffset.Parse(s);
        }


        public static void SetTimeSpan(this ISettingsService settings, string key, TimeSpan ts) {
            settings.Set(key, ts.Ticks.ToString());
        }


        public static TimeSpan GetTimeSpan(this ISettingsService settings, string key) {
            var num = settings.GetLong(key, 0);
            return TimeSpan.FromTicks(num);
        }
    }
}
