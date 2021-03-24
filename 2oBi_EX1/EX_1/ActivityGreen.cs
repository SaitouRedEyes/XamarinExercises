using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EX_1
{
    [Activity(Label = "ActivityGreen")]
    public class ActivityGreen : Activity
    {
        private bool isPaused;
        private LinearLayout llGreen;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_green);

            isPaused = false;
            llGreen = FindViewById<LinearLayout>(Resource.Id.ll_green);

            Button b3 = FindViewById<Button>(Resource.Id.b3);

            b3.Click += Onb3Clicked;
        }

        private void Onb3Clicked(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));

            StartActivity(i);
            Finish();
        }

        protected override void OnPause()
        {
            base.OnPause();

            isPaused = true;
        }

        protected override void OnResume()
        {
            base.OnResume();

            if(isPaused == true)
            {
                llGreen.SetBackgroundColor(new Android.Graphics.Color(80, 80, 80));
                isPaused = false;
            }
        }
    }

    
}