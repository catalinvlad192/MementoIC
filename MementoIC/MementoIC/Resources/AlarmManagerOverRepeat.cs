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
using MementoIC;
using static Android.Resource;

namespace TaskManager
{
    [BroadcastReceiver(Enabled = true)]
    class AlarmManagerOverRepeat : BroadcastReceiver
    {
        static int NOTIFICATION_ID = 1000;

        public override void OnReceive(Context context, Intent intent)
        {

            foreach (Task t in MainActivity.items)
            {
                //if ((t.deadline.Year <= DateTime.UtcNow.Year && t.status == false) ||
                //    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month <= DateTime.UtcNow.Month && t.status == false) ||
                //    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month == DateTime.UtcNow.Month && t.deadline.Day <= DateTime.UtcNow.Day && t.status == false) ||
                //    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month == DateTime.UtcNow.Month && t.deadline.Day == DateTime.UtcNow.Day && t.deadline.Hour <= DateTime.UtcNow.Hour && t.status == false) ||
                //    (t.deadline.Year == DateTime.UtcNow.Year && t.deadline.Month == DateTime.UtcNow.Month && t.deadline.Day == DateTime.UtcNow.Day && t.deadline.Hour == DateTime.UtcNow.Hour && t.deadline.Minute <= DateTime.UtcNow.Minute && t.status == false))

                //current date and time
                DateTime now = DateTime.UtcNow;
                DateTime nowOrig = now.AddHours(3);

                //compare current date with deadline
                if (t.deadline.CompareTo(nowOrig) <= 0 && t.status == false)
                {
                    //id from HashCode
                    int idHash = t.deadline.GetHashCode();

                    //intent to main activity
                    Intent intentToMainA = new Intent(context, typeof(MainActivity));

                    //pending intent
                    PendingIntent pendingIntentToMainA = PendingIntent.GetActivity(context, idHash, intentToMainA, PendingIntentFlags.OneShot);

                    //building the notification
                    Notification.Builder notificationBuilder = new Notification.Builder(context)
               .SetContentIntent(pendingIntentToMainA)
               .SetSmallIcon(Resource.Drawable.wall)
               .SetContentTitle(t.name)
               .SetContentText(t.description)
               .SetDefaults(NotificationDefaults.Vibrate);


                    //manage the notification
                    var notificationM = (NotificationManager)context.GetSystemService(Context.NotificationService);

                    //start notification
                    notificationM.Notify(idHash, notificationBuilder.Build());

                }
            }
        }
    }
}
