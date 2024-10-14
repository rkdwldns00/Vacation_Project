using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidFeatureManager : MonoBehaviour
{
    private const string PUSH_DEFAULT_CHANNEL_ID = "default_channel";

    private static AndroidFeatureManager _instance;

    public static AndroidFeatureManager Instance
    {
        get
        {
            if (_instance is null)
            {
                GameObject g = new GameObject("AndroidFeatureManager");
                _instance = g.AddComponent<AndroidFeatureManager>();
                DontDestroyOnLoad(g);
            }
            return _instance;
        }
    }

    private AndroidJavaClass _javaClass = null;
    private AndroidJavaObject _javaClassInstance = null;

    private void Awake()
    {
        _javaClass = new AndroidJavaClass("com.example.plugin.Plugin");
        if (_javaClass is not null)
        {
            _javaClassInstance = _javaClass.CallStatic<AndroidJavaObject>("GetInstance");
        }

        RegisterNotificationChannel(PUSH_DEFAULT_CHANNEL_ID, "Default Channel");
    }

    public void ShowToast(string massage)
    {
        _javaClassInstance.Call("ShowToast", massage);
    }

    public void Share(string text)
    {
        _javaClassInstance.Call("Share", text, "");
    }

    public void RegisterNotificationChannel(string channelId, string channelName = "Channel Name", string channelDescription = "channel description")
    {
        var c = new AndroidNotificationChannel()
        {
            Id = channelId,
            Name = channelName,
            Importance = Importance.High,
            Description = channelDescription,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    public void SendNotifycation(string title, string explain, string iconId, DateTime time, string channelId = PUSH_DEFAULT_CHANNEL_ID)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = explain;
        notification.FireTime = time;

        notification.SmallIcon = iconId;
        notification.LargeIcon = "main_icon";

        //notification.IntentData = "{\"title\": \"Notification 1\", \"data\": \"200\"}";

        AndroidNotificationCenter.SendNotification(notification, channelId);
    }
}
