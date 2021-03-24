﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace EX_1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button b1 = FindViewById<Button>(Resource.Id.b1);

            b1.Click += Onb1Clicked;
        }

        private void Onb1Clicked(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(ActivityGreen));

            StartActivity(i);
        }
    }
}