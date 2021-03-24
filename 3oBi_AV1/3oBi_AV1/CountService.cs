using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Widget;
using Java.Lang;

namespace _3oBi_AV1
{
    [Service(Name = "com._3oBi_AV1.CountService")]
    class CountService : Service, IRunnable, ICount
    {
        static readonly string TAG = typeof(CountService).FullName;

        private int countHungry, countThirsty;
        private bool hungryNotification, thirstyNotification, deadNotification;
        private Handler h;
        private NotificationManagerCompat nmc;

        public IBinder Binder { get; private set; }

        public override void OnCreate()
        {
            base.OnCreate();

            Log.Debug("CountService", "OnCreate");
            deadNotification = thirstyNotification = hungryNotification = false;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if(!deadNotification && !thirstyNotification && !hungryNotification)
            {
                nmc = NotificationManagerCompat.From(this);
                nmc.Cancel(1000);

                countHungry = 30;
                countThirsty = 100;

                Log.Debug("CountService", "OnStartCommand - Start Service Notification");

                h = new Handler();
                h.Looper.Thread.Name = "CountService";
                h.Post(this);

                SendMyNotification(Resources.GetString(Resource.String.app_name), "Start Service", "service_notification", "service_notification", "start service notification", true);
            }
            
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug("CountService", "OnBind - Binding Service");
            Binder = new CountBinder(this);
            return Binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            Log.Debug("CountService", "OnUnbind - Unbinding Service");
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            Binder = null;

            nmc.Cancel(10000);
            StopSelf();
            StopForeground(true);

            Log.Debug("CountService", "OnDestroy - Stop foreground service");

            base.OnDestroy();
        }

        public void Run()
        {
            if (!deadNotification)
            {
                countHungry--;
                countThirsty--;

                if (countHungry < 25 && !hungryNotification)
                {
                    hungryNotification = true;
                    SendMyNotification("FOMEEEEE", "Alimente seu POU!!", "count_notification", "count_notification", "hungry notification", false);
                }
                else if (countThirsty < 25 && !thirstyNotification)
                {
                    thirstyNotification = true;
                    SendMyNotification("SEDEEEEE", "Dê água a seu POU!!", "count_notification", "count_notification", "thirsty notification", false);
                }
                else if ((countHungry <= 0 || countThirsty <= 0) && !deadNotification)
                {
                    deadNotification = true;
                    hungryNotification = thirstyNotification = false;
                    SendMyNotification("ADEUSSSS", "Seu POU faleceu", "count_notification", "count_notification", "death notification", false);
                }

                Log.Debug("CountService", "RUN: " + countHungry.ToString());

                h.PostDelayed(this, 1000);
            }

            Log.Debug("CountService", "RUN OFF");
        }

        public int CountHungry
        {
            get { return countHungry; }
            set {
                if (value == 5 && countHungry <= 95)
                {
                    countHungry += value;

                    if (countHungry > 25) hungryNotification = false;
                }
            }
        }

        public int CountThirsty
        {
            get { return countThirsty; }
            set {
                if (value == 5 && countThirsty <= 95)
                {
                    countThirsty += value;

                    if(countThirsty > 25) thirstyNotification = false;
                }
            }
        }

        public bool DeathStats
        {
            get { return deadNotification; }
            set { deadNotification = value; }
        }

        private void SendMyNotification(string title, string description, string channelID, string channelName, string channelDescription, bool serviceNotification)
        {
            // When the user clicks the notification, SecondActivity will start up.
            Intent resultIntent = new Intent(this, typeof(MainActivity));
            PendingIntent resultPendingIntent;

            if(serviceNotification) resultPendingIntent = null;
            else
            {
                resultIntent.SetAction("Main_Activity");
                resultIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);

                // Construct a back stack for cross-task navigation:
                Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                stackBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));
                stackBuilder.AddNextIntent(resultIntent);

                // Create the PendingIntent with the back stack:            
                resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
            }
            
            //Build Notification
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channelID)
                .SetAutoCancel(true)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle(title)
                .SetSmallIcon(Resource.Drawable.notification_icon_background)
                .SetOngoing(true)
                .SetContentText(string.Format(description));

            NotificationManager nf = (NotificationManager)GetSystemService(NotificationService);

            //Creating a Channel
            if (Build.VERSION.SdkInt > BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the support library). There is no need to create a notification
                // channel on older versions of Android.
                NotificationChannel nc = new NotificationChannel(channelID, channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                nf.CreateNotificationChannel(nc);
            }

            //Send Notification
            if (serviceNotification)
            {
                StartForeground(10000, builder.Build());
            }
            else
            {
                nmc = NotificationManagerCompat.From(this);
                nmc.Notify(1000, builder.Build());
            }
        }
    }
}