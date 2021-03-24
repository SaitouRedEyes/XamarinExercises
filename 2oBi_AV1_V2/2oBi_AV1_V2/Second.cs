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

namespace _2oBi_AV1_V2
{
    [Activity(Label = "Second")]
    public class Second : Activity
    {        
        LinearLayout mainLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.second);

            mainLayout = (LinearLayout)FindViewById(Resource.Id.ll);

            Intent i = Intent;
            if (i != null && i.Extras != null)
            {
                Bundle myParameters = i.Extras;
                
                switch(myParameters.GetString("btn"))
                {
                    case "yellow": mainLayout.SetBackgroundColor(new Android.Graphics.Color(255, 255, 0)); break;
                    case "red": mainLayout.SetBackgroundColor(new Android.Graphics.Color(255, 0, 0)); break;
                    case "blue" : mainLayout.SetBackgroundColor(new Android.Graphics.Color(0, 0, 255)); break;
                }
            }
        }

        protected override void OnPause()
        {
            base.OnPause();            
            mainLayout.SetBackgroundColor(new Android.Graphics.Color(80, 80, 80));
        }        
    }
}