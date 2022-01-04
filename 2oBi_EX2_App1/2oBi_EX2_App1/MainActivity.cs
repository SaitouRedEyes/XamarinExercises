using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace _2oBi_EX2_App1
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

            Button green = FindViewById<Button>(Resource.Id.green);
            Button blue = FindViewById<Button>(Resource.Id.blue);
            Button red = FindViewById<Button>(Resource.Id.red);

            green.Click += OnGreenButtonClicked;
            blue.Click += OnBlueButtonClicked;
            red.Click += OnRedButtonClicked;                        
        }

        private void OnRedButtonClicked(object sender, EventArgs e)
        {
            ChangeActivity("red");            
        }

        private void OnBlueButtonClicked(object sender, EventArgs e)
        {
            ChangeActivity("blue");            
        }

        private void OnGreenButtonClicked(object sender, EventArgs e)
        {
            ChangeActivity("green");            
        }

        private void ChangeActivity(string color)
        {
            i = new Intent("TrocaCor");
            i.AddCategory(color);
            StartActivity(i);
        }
    }
}