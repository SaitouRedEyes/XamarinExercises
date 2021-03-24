using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace _2oBi_AV1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Intent i;
        Bundle parameters;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button btFB = FindViewById<Button>(Resource.Id.loginFB);
            Button btGmail = FindViewById<Button>(Resource.Id.loginGmail);

            btFB.Click += OnBtnFBClicked;
            btGmail.Click += OnBtnGmailClicked;

            i = new Intent(this, typeof(Profile));
            parameters = new Bundle();
        }

        private void OnBtnGmailClicked(object sender, EventArgs e)
        {
            parameters.PutString("typeLogin", "gmail");
            i.PutExtras(parameters);

            StartActivity(i);
        }

        private void OnBtnFBClicked(object sender, EventArgs e)
        {
            parameters.PutString("typeLogin", "fb");
            i.PutExtras(parameters);

            StartActivity(i);
        }

        protected override void OnResume()
        {
            base.OnResume();
            parameters.Clear();
        }
    }
}