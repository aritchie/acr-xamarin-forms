using System;
using Android.App;
using Android.Content;
using Android.Net;


namespace Acr.XamForms.Network.Droid {

    [BroadcastReceiver(Enabled = true, Label = "Network Status Receiver")]
    [IntentFilter(new string[] { "android.net.conn.CONNECTIVITY_CHANGE" })] 
    public class NetworkConnectionBroadcastReceiver : BroadcastReceiver {
        internal static Action<NetworkInfo> OnChange { get; set; }


        public override void OnReceive(Context context, Intent intent) {
            if (intent.Extras == null || OnChange == null)
                return;

            var ni = intent.Extras.Get(ConnectivityManager.ExtraNetworkInfo) as NetworkInfo;
            if (ni == null)
                return;

            OnChange(ni);
        }
    }
}