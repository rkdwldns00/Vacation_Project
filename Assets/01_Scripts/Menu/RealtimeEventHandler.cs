using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 강지운 */
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
    public event Action<float> OnUpdate;

    public int IntervalWithLastLoginDay { get; private set; }
    public bool isFirstLogin { get; private set; }
    private float savedVersion
    {
        get => PlayerPrefs.GetFloat("version");
        set => PlayerPrefs.SetFloat("version", value);
    }

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
        if (TutorialManager.isActive)
        {
            gameObject.SetActive(false);
            return;
        }

        float curruntVersion = float.Parse(Application.version);
        if (isFirstLogin)
        {
            savedVersion = curruntVersion;
            OnFirstLogin?.Invoke();
        }
        else if (savedVersion != curruntVersion)
        {
            float originVersion = savedVersion;
            savedVersion = curruntVersion;
            OnUpdate?.Invoke(originVersion);
        }
    }

    private void Update()
    {
        today = TicksToDay(DateTime.Now.Ticks);
        int lastLoginDay = PlayerPrefs.GetInt("lastLoginDay");
        IntervalWithLastLoginDay = today - lastLoginDay;

        if (IntervalWithLastLoginDay > 0)
        {
            OnChangeDay?.Invoke();
            PlayerPrefs.SetInt("lastLoginDay", today);
        }
    }

    private int TicksToDay(long tick)
    {
        return (int)(tick / 10000000 / 60 / 60 / 24);
    }

    [ContextMenu("날짜 변경 이벤트 활성화")]
    public void RunChangeDayEvent()
    {
        PlayerPrefs.SetInt("lastLoginDay", today - 1);
    }

    [ContextMenu("업데이트 이벤트 활성화")]
    public void RunUpdateEvent()
    {
        savedVersion = 0;
    }
}
