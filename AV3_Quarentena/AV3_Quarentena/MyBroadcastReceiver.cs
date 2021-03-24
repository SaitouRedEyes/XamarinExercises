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
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "MyBroadcastReceiver" }, Categories = new[] { Intent.CategoryDefault })]
    public class MyBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Bundle myParameters = intent.Extras;

            if (myParameters != null)
            {                
                if (myParameters.Size() > 0)
                {
                    if(!myParameters.GetString("Random").Equals("0"))
                    {
                        Intent i = new Intent("ActivityResultFilter");
                        i.PutExtras(myParameters);
                        context.StartActivity(i);
                    }                    
                    else
                    {
                        Toast.MakeText(context, "Parameters send couldn't be used", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(context, "Parameters without data", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(context, "Intent without parameters received", ToastLength.Short).Show();
            }
        }
    }
}