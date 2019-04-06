using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
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
                //Current date and time
                DateTime now = DateTime.UtcNow;
                DateTime nowOrig = now.AddHours(3);

                //Compare current date with deadline
                if (t.deadline.CompareTo(nowOrig) <= 0 && t.status == false)
                {
                    //Id from HashCode
                    int idH = t.deadline.GetHashCode();

                    //Intent to main activity
                    Intent intentToMainA = new Intent(context, typeof(MainActivity));

                    //Pending intent
                    PendingIntent pendingIntentToMainA = PendingIntent.GetActivity(context, idH, intentToMainA, PendingIntentFlags.OneShot);


                    if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                    {
                        // Notification channels are new in API 26 (and not a part of the support library)

                        //Manage the notification
                        var notificationM = (NotificationManager)context.GetSystemService(Context.NotificationService);

                        //Building the notification
                        Notification.Builder notificationBuilder = new Notification.Builder(context)
                                            .SetContentIntent(pendingIntentToMainA)
                                            .SetSmallIcon(Resource.Drawable.wall)
                                            .SetContentTitle(t.name)
                                            .SetContentText(t.description)
                                            .SetDefaults(NotificationDefaults.Vibrate);

                        //start notification
                        notificationM.Notify(idH, notificationBuilder.Build());

                        return;
                    }

                    //Id from HashCode
                    string idHash = t.deadline.GetHashCode().ToString();
                    var channelName = "Memento";
                    var channelDescription = "Deadline reached";
                    var channel = new NotificationChannel(idHash, channelName, NotificationImportance.Default)
                    {
                        Description = channelDescription
                    };

                    // Instantiate the builder and set notification elements, including pending intent:
                    NotificationCompat.Builder builder = new NotificationCompat.Builder(context, idHash)
                        .SetContentIntent(pendingIntentToMainA)
                        .SetContentTitle(t.name)
                        .SetContentText(t.description)
                  //      .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                        .SetSmallIcon(Resource.Drawable.wall);

                    Notification notification = builder.Build();

                    var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService) as NotificationManager;
                    notificationManager.CreateNotificationChannel(channel);

                    //start notification
                    notificationManager.Notify(idH, notification);
                }
            }
        }
    }
}
