using Android.Content;
using Android.OS;
using Android.Util;

namespace _3oBi_AV1
{
    class CountServiceConnection : Java.Lang.Object, IServiceConnection
    {
        static readonly string TAG = typeof(CountServiceConnection).FullName;
        MainActivity mainActivity;

        public CountServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            Binder = null;
            mainActivity = activity;
        }

        public bool IsConnected { get; private set; }
        public CountBinder Binder { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as CountBinder;
            IsConnected = this.Binder != null;

            Log.Debug("CountServiceConnection", "OnServiceConnected");

            if (IsConnected) mainActivity.UpdateUiForBoundService();
            else mainActivity.UpdateUiForUnboundService();
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Log.Debug("CountServiceConnection", "OnServiceDisconnected");

            IsConnected = false;
            Binder = null;
            mainActivity.UpdateUiForUnboundService();
        }
    }
}