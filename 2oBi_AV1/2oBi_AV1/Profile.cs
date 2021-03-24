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

namespace _2oBi_AV1
{
    [Activity(Label = "Profile")]
    public class Profile : Activity
    {
        string typeLogin;
        TextView tvTypeLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profile_layout);

            Intent i = Intent;

            if(i != null && i.Extras != null)
            {
                typeLogin = i.Extras.GetString("typeLogin");
            }

            tvTypeLogin = FindViewById<TextView>(Resource.Id.loginProfileWay);
            tvTypeLogin.Text = typeLogin;
        }

        protected override void OnPause()
        {
            base.OnPause();
            tvTypeLogin.Text = "Undefined";
        }
    }
}