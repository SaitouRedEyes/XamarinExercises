using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace _3oBi_AV1
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity, IRunnable
    {
        private Handler h;
        private CountServiceConnection connection;
        private Button btHungry, btThirsty, btRestart;
        private TextView tvCountHungry, tvCountThirsty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            btHungry = (Button)FindViewById(Resource.Id.btHungry);
            btThirsty = (Button)FindViewById(Resource.Id.btThirsty);
            btRestart = (Button)FindViewById(Resource.Id.btRestart);

            tvCountHungry = (TextView)FindViewById(Resource.Id.tvHungry);
            tvCountThirsty = (TextView)FindViewById(Resource.Id.tvThirsty);

            btHungry.Click += OnBtHungryClicked;
            btThirsty.Click += OnBtThirstyClicked;
            btRestart.Click += OnBtRestartClicked;

            StartMyService();
        }

        private void OnBtRestartClicked(object sender, EventArgs e)
        {
            connection.Binder.Service.DeathStats = false;
            StartMyService();

            btRestart.Visibility = Android.Views.ViewStates.Invisible;
        }

        private void OnBtThirstyClicked(object sender, EventArgs e)
        {
            if (connection.IsConnected && !connection.Binder.Service.DeathStats)
                connection.Binder.Service.CountThirsty = 5;
        }

        private void OnBtHungryClicked(object sender, EventArgs e)
        {
            if (connection.IsConnected && !connection.Binder.Service.DeathStats)
                connection.Binder.Service.CountHungry = 5;
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (connection == null) connection = new CountServiceConnection(this);

            BindService(new Intent(this, typeof(CountService)), connection, Bind.AutoCreate);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (connection.IsConnected) UpdateUiForBoundService();
            else UpdateUiForUnboundService();
        }

        protected override void OnStop()
        {
            UnbindService(connection);
            base.OnStop();
        }

        public void UpdateUiForBoundService()
        {
            Log.Debug("MainActivity", "Update UI for BOUND SERVICE");
            h = new Handler();
            h.Looper.Thread.Name = "MainActivity";
            h.Post(this);
        }

        public void UpdateUiForUnboundService()
        {
            Log.Debug("MainActivity", "Update UI for UNBOUND SERVICE");
        }

        private void StartMyService()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O) StartForegroundService(new Intent(this, typeof(CountService)));
            else StartService(new Intent(this, typeof(CountService)));
        }

        public void Run()
        {
            if(connection.IsConnected)
            {
                tvCountHungry.Text = "Comida: " + connection.Binder.Service.CountHungry;
                tvCountThirsty.Text = "Sede: " + connection.Binder.Service.CountThirsty;

                if (connection.Binder.Service.CountHungry <= 0 || connection.Binder.Service.CountThirsty <= 0)
                {
                    btRestart.Visibility = ViewStates.Visible;
                }
                else
                {
                    btRestart.Visibility = ViewStates.Invisible;
                }

                Log.Debug("MainActivity", "RUN: " + tvCountHungry.Text + " Name: " + h.Looper.Thread.Name);

                h.PostDelayed(this, 1000);
            }
        }
    }
}