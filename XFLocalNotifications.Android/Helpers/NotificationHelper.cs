using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using XFLocalNotifications.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]
namespace XFLocalNotifications.Droid.Helpers
{
    internal class NotificationHelper : INotification
    {
        private static string foregroundChannelId = "9001";
        private static Context context = global::Android.App.Application.Context;


        public Notification ReturnNotif()
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("Title", "Message");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            var notifBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                .SetContentTitle("Thông Báo")
                .SetContentText("Một máy may đang gặp vấn đề")
                .SetSmallIcon(Resource.Drawable.location)
                
                .SetOngoing(true)
                .SetContentIntent(pendingIntent)
                .SetAutoCancel(true);

            //if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            //{
            //    NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.Low);
            //    notificationChannel.Importance = NotificationImportance.High;
            //    notificationChannel.EnableLights(true);
            //    notificationChannel.EnableVibration(true);
            //    notificationChannel.SetShowBadge(true);
               
            //    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300 });

            //    var notifManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            //    if (notifManager != null)
            //    {
            //        notifBuilder.SetChannelId(foregroundChannelId);
            
            //        notifManager.CreateNotificationChannel(notificationChannel);
            //    }
            //}

            return notifBuilder.Build();
        }
    }
}