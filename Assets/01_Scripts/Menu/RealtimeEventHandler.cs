using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeEventHandler : MonoBehaviour
{
    private static RealtimeEventHandler _instance;
    public static RealtimeEventHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RealtimeEventHandler>();
                if (_instance == null)
                {
                    GameObject g = new GameObject("RealtimeEventHandler");
                    _instance = g.AddComponent<RealtimeEventHandler>();
                }
            }
            return _instance;
        }
    }

    public event Action OnChangeDay;
    public event Action OnFirstLogin;

    public int IntervalWithLastLoginDay { get; private set; }
    public bool isFirstLogin { get; private set; }

    private int today;

    private void Awake()
    {
        today = TicksToDay(DateTime.Now.Ticks);
        int lastLoginDay = PlayerPrefs.GetInt("lastLoginDay");
        isFirstLogin = lastLoginDay == 0;
        IntervalWithLastLoginDay = today - lastLoginDay;
    }

    private void Start()
    {
        if (TutorialManager.isActive) return;

        PlayerPrefs.SetInt("lastLoginDay", today);
        if (isFirstLogin)
        {
            OnFirstLogin?.Invoke();
        }
        if (IntervalWithLastLoginDay > 0)
        {
            OnChangeDay?.Invoke();
        }
    }

    private int TicksToDay(long tick)
    {
        return (int)(tick / 10000000 / 60 / 60 / 24);
    }

    [ContextMenu("날짜 변경")]
    public void RunChangeDayEvent()
    {
        PlayerPrefs.SetInt("lastLoginDay", today - 1);
    }
}
