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

namespace BroadcastEX
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "MeuBR" }, Categories = new[] { Intent.CategoryDefault })]
    public class MeuBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Bundle myParameters = intent.Extras;

            if (myParameters != null)
            {
                Toast.MakeText(context, myParameters.GetString("milho"), ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(context, "A mensagem recebida não possui conteúdo extra", ToastLength.Long).Show();
            }            
        }
    }
}