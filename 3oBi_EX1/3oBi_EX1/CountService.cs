using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace _3oBi_EX1
{
    [Service(IsolatedProcess = false, Name = "com._3oBi_EX1.CountService")]
    class CountService : Service, IRunnable
    {
        protected int count, currLimit, maxLimit;
        public static bool active;
        private Handler h;

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();

            active = true;
            currLimit = 10;
            maxLimit = 30;

            h = new Handler();
            h.Post(this);

            Log.Debug("Counter Service", "OnCreate");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            Log.Debug("Counter Service", "OnDestroy");
        }

        public void Run()
        {
            if (active)
            {
                Log.Debug("Counter Service", "Count: " + count);
                count++;

                if(count >= currLimit)
                {
                    if (currLimit < maxLimit)
                    {
                        SendMyNotification("Limite Excedido", "O limite {0} foi excedido.");
                        count = 0;
                        currLimit += 5;
                    }
                    else
                    {
                        SendMyNotification("Game Over", "O limite máximo de {0} foi excedido");
                        active = false;
                    }
                }

                h.PostDelayed(this, 1000);
            }
            else
            {
                count = 0;
                currLimit = 10;
                Log.Debug("Counter Service", "Service End!!");

                StopSelf();
            }
        }

        private void SendMyNotification(string title, string description)
        {
            //Build Notification
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, "count_notification")
                .SetAutoCancel(true)
                //.SetContentIntent(resultPendingIntent)
                .SetContentTitle(title)
                .SetNumber(count)
                .SetSmallIcon(Resource.Drawable.notification_icon_background)
                .SetContentText(string.Format(description, count));

            NotificationManager nf = (NotificationManager)GetSystemService(NotificationService);

            //Creating a Channel
            if (Build.VERSION.SdkInt > BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                string channelName = "Canal da notificador do contador";
                string channelDescription = "Minha descrição do canal";
                NotificationChannel nc = new NotificationChannel("count_notification", channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                nf.CreateNotificationChannel(nc);
            }

            //Send Notification
            NotificationManagerCompat nmc = NotificationManagerCompat.From(this);
            nmc.Notify(1000, builder.Build());
        }
    }
}