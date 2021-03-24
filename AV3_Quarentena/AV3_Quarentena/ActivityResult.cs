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

namespace AV3_Quarentena
{
    [Activity(Label = "ActivityResult")]
    [IntentFilter(new[] { "ActivityResultFilter" }, Categories = new[] { Intent.CategoryDefault })]
    public class ActivityResult : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_result);

            LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.myLinearLayoutActivityResult);

            Bundle myParameters = Intent.Extras;

            switch(myParameters.GetString("Random"))
            {
                case "1": ll.SetBackgroundColor(new Android.Graphics.Color(255, 0, 0)); break;
                case "2": ll.SetBackgroundColor(new Android.Graphics.Color(0, 255, 0)); break;
                case "3": ll.SetBackgroundColor(new Android.Graphics.Color(0, 0, 255)); break;
                case "4": ll.SetBackgroundColor(new Android.Graphics.Color(255, 255, 0)); break;
            }            
        }
    }
}