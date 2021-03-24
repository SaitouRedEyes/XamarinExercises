using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace AV3_Quarentena
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MyBroadcastReceiver receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            receiver = new MyBroadcastReceiver();            

            Button btnSendBroadcast = (Button)FindViewById(Resource.Id.sendBroadcast);
            btnSendBroadcast.Click += OnbtnSendBroadcastClicked;
        }

        private void OnbtnSendBroadcastClicked(object sender, EventArgs e)
        {
            Intent i = new Intent("MyBroadcastReceiver");
            Random generator = new Random();

            Bundle myParameters = new Bundle();
            myParameters.PutString("Random", generator.Next(0, 5).ToString());

            i.PutExtras(myParameters);

            SendBroadcast(i);
        }

        protected override void OnResume()
        {
            base.OnResume();
            IntentFilter myFilter = new IntentFilter("MyBroadcastReceiver");
            RegisterReceiver(receiver, myFilter);
        }

        protected override void OnPause()
        {           
            UnregisterReceiver(receiver);
            base.OnPause();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}