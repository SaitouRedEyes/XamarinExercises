using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EX_1
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        LinearLayout meuLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.second);

            meuLayout = FindViewById<LinearLayout>(Resource.Id.secondLayout);

        }

        protected override void OnPause()
        {
            base.OnPause();
            meuLayout.SetBackgroundColor(new Android.Graphics.Color(80, 80, 80));
        }
    }
}