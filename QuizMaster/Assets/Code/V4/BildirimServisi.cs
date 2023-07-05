using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class BildirimServisi : MonoBehaviour
{
    int Flag = 0;
    private void OnApplicationPause(bool isApplicationPause)
    {
        if(Flag == 0)
        {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id2",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Son Harf";
        notification.Text = "Rakibin cevap vermiş olabilir. Hemen oyuna gir ve cevap ver";
        notification.SmallIcon = "kucuk";
        notification.LargeIcon = "buyuk";
        notification.FireTime = System.DateTime.Now.AddMinutes(0.1f);

        AndroidNotificationCenter.SendNotification(notification, "channel_id2");
        Flag = 1;
        }
    }
}
