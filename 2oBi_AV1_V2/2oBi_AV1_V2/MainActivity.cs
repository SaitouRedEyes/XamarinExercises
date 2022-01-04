using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Util;
using Android.Content;

namespace _2oBi_AV1_V2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button btn1 = (Button)FindViewById(Resource.Id.btn1);
            Button btn2 = (Button)FindViewById(Resource.Id.btn2);            

            btn1.Tag = "btn1";
            btn2.Tag = "btn2";            

            btn1.Click += GoToSecond;
            btn2.Click += GoToSecond;            
        }

        private void GoToSecond(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(Second));
            Bundle myParameters = new Bundle();            

            switch ((string)((Button)sender).Tag)
            {
                case "btn1": myParameters.PutString("btn", "yellow"); break;
                case "btn2": myParameters.PutString("btn", "red"); break;                
            }

            i.PutExtras(myParameters);
            StartActivity(i);
            //Log.Debug("NAME", myParameters.GetString("btn"));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}