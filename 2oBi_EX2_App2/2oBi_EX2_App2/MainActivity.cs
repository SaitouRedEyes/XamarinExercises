using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace _2oBi_EX2_App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    [IntentFilter(new[] {"TrocaCor"}, Categories = new[] { Intent.CategoryDefault, "red", "green"})]
    public class MainActivity : Activity
    {
        LinearLayout ll;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            ll = FindViewById<LinearLayout>(Resource.Id.mainLayout);

            Intent i = Intent;

            if (i.Categories.Contains("red"))
            {
                ll.SetBackgroundColor(new Android.Graphics.Color(255, 0, 0));
            }

            if (i.Categories.Contains("green"))
            {
                ll.SetBackgroundColor(new Android.Graphics.Color(0, 255, 0));
            }
        }        
	}
}

