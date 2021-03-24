using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace _2oBi_EX2_App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    [IntentFilter(new[] {"TrocaCor"}, Categories = new[] { Intent.CategoryDefault})]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.mainLayout);

            Intent i = Intent;

            if(i != null && i.Extras != null)
            {
                switch(i.Extras.GetString("color"))
                {
                    case "red": ll.SetBackgroundColor(new Android.Graphics.Color(255, 0, 0)); break;
                    case "green": ll.SetBackgroundColor(new Android.Graphics.Color(0, 255, 0)); break;
                    case "blue": ll.SetBackgroundColor(new Android.Graphics.Color(0, 0, 255)); break;
                }
            }
        }
	}
}

