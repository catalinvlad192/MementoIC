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
using Android.Util;
using MementoIC;

namespace TaskManager
{
    [Service]
    public class NotificationService : Service
    {
        static readonly string TAG = typeof(NotificationService).FullName;
        static readonly int DELAY_BETWEEN_LOG_MESSAGES = 5000; // milliseconds
        static int NOTIFICATION_ID = 1000;

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Info(TAG, "OnCreate: the service is initializing.");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            AlarmManager am = (AlarmManager)GetSystemService(Context.AlarmService);
            Intent alarmIntent;
            PendingIntent alarmPendingIntent;
            alarmIntent = new Intent(this, typeof(AlarmManagerOverRepeat));
            alarmPendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, 0);
            am.SetRepeating(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 10 * 1000, 60 * 1000, alarmPendingIntent);

            // This tells Android to restart the service if it is killed to reclaim resources.
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        void DispatchNotificationThatServiceIsRunning()
        {
            foreach (Task t in MainActivity.items)
            {
                if ((t.deadline.Year <= DateTime.UtcNow.Year && t.status == false) ||
                    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month <= DateTime.UtcNow.Month && t.status == false) ||
                    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month == DateTime.UtcNow.Month && t.deadline.Day <= DateTime.UtcNow.Day && t.status == false) ||
                    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month == DateTime.UtcNow.Month && t.deadline.Day == DateTime.UtcNow.Day && t.deadline.Hour <= DateTime.UtcNow.Hour && t.status == false) ||
                    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month == DateTime.UtcNow.Month && t.deadline.Day == DateTime.UtcNow.Day && t.deadline.Hour == DateTime.UtcNow.Hour && t.deadline.Minute <= DateTime.UtcNow.Minute && t.status == false))
                {

                    if (    true    /*Build.VERSION.SdkInt < BuildVersionCodes.O*/)
                    {
                        string s = "Version ";
                        s = s + Build.VERSION.SdkInt;
                        Toast.MakeText(this, s, ToastLength.Short).Show();
                        // Notification channels are new in API 26 (and not a part of the
                        // support library). There is no need to create a notification
                        // channel on older versions of Android.
 //                       return;
                    }
                    Notification.Builder notificationBuilder = new Notification.Builder(this)
               .SetSmallIcon(Resource.Drawable.wall)
               .SetContentTitle(t.name)
               .SetContentText(t.description);

                    var notificationM = (NotificationManager)GetSystemService(NotificationService);
                    notificationM.Notify(NOTIFICATION_ID, notificationBuilder.Build());
                    NOTIFICATION_ID++;
                }
            }
        }
    }
}