using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace BroadcastEX
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MeuBroadcastReceiver receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            Button btnMandaBroadcast = FindViewById<Button>(Resource.Id.btnMandaBroadcast);
            Button btnMandaSemNada = FindViewById<Button>(Resource.Id.btnMandaSemNada);

            btnMandaBroadcast.Click += MandaBroadcast;
            btnMandaSemNada.Click += MandaBroadcastSemNada;            

            receiver = new MeuBroadcastReceiver();
        }

        private void MandaBroadcastSemNada(object sender, EventArgs e)
        {
            Intent i = new Intent("MeuBR");            
            SendBroadcast(i);
        }

        private void MandaBroadcast(object sender, EventArgs e)
        {
            Intent i = new Intent("MeuBR");

            Bundle myParameters = new Bundle();
            myParameters.PutString("milho", "Olá 3004");            

            i.PutExtras(myParameters);

            SendBroadcast(i);
        }

        protected override void OnResume()
        {
            base.OnResume();
            IntentFilter myFilter = new IntentFilter("MeuBR");
            RegisterReceiver(receiver, myFilter);
        }

        protected override void OnPause()
        {
            UnregisterReceiver(receiver);
            base.OnPause();
        }
    }
}