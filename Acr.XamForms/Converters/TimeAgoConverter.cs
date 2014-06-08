//using System;
//using System.Globalization;
//using Cirrious.CrossCore.Converters;


//namespace Acr.XamForms.Converters {

//    public enum TimeAgo {
//        Now,
//        Seconds,
//        Minutes,
//        Hours,
//        Days
//    }


//    public class TimeAgoConverter : MvxValueConverter<DateTime> {

//        protected override object Convert(DateTime when, Type targetType, object parameter, CultureInfo culture) {
//            var difference = (DateTime.UtcNow - when).TotalSeconds;
//            TimeAgo format;
//            int value = 0;

//            if (difference < 30.0) {
//                format = TimeAgo.Now;
//            }
//            else if (difference < 100) {
//                format = TimeAgo.Seconds;
//                value = (int)difference;
//            }
//            else if (difference < 3600) {
//                format = TimeAgo.Minutes;
//                value = (int)(difference / 60);
//            }
//            else if (difference < 24 * 3600) {
//                format = TimeAgo.Hours;
//                value = (int)(difference / (3600));
//            }
//            else {
//                format = TimeAgo.Days;
//                value = (int)(difference / (3600 * 24));
//            }

//            return this.GetString(format, value);
//        }


//        protected virtual string GetString(TimeAgo timeAgo, int value) {
//            if (timeAgo == TimeAgo.Now)
//                return "Just Now";

//            return String.Format("{0} {1} ago", value, timeAgo.ToString().ToLower());
//        }
//    }
//}