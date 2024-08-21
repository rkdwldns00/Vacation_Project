using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class Currency
{
    private static int _gold = PlayerPrefs.GetInt("gold");
    private static int _crystal = PlayerPrefs.GetInt("crystal");
    private static int _ticket = PlayerPrefs.GetInt("ticket");

    public static Action<int> OnChangedGold;
    public static Action<int> OnChangedCrystal;
    public static Action<int> OnChangedTicket;

    public static int Gold
    {
        get => _gold;
        set
        {
            int origin = _gold;
            _gold = value;
            PlayerPrefs.SetInt("gold", value);

            OnChangedGold?.Invoke(value - origin);
        }
    }

    public static int Crystal
    {
        get => _crystal;
        set
        {
            int origin = _crystal;
            _crystal = value;
            PlayerPrefs.SetInt("crystal", value);

            OnChangedCrystal?.Invoke(value - origin);
        }
    }

    public static int MaxTicket => 5;

#if UNITY_EDITOR
    public static int TicketHealTime => 30;
#else
    public static int TicketHealTime => 300;
#endif
    private static int RealTimeTick => (int)((DateTime.Now.Ticks / 10000000) % 1000000000);

    public static int Ticket
    {
        get
        {
            return _ticket;
        }
        set
        {
            if (_ticket >= MaxTicket && value < MaxTicket)
            {
                PlayerPrefs.SetInt("TicketHealStartTime", RealTimeTick);
            }

            int origin = _ticket;
            _ticket = value;
            PlayerPrefs.SetInt("ticket", value);

            OnChangedTicket?.Invoke(value - origin);
        }
    }

    public static int TicketHealingTimer
    {
        get
        {
            return _TicketHealingTimer;
        }
    }

    private static int _TicketHealingTimer => RealTimeTick - PlayerPrefs.GetInt("TicketHealStartTime");

    public static void UpdateTicket()
    {
        if (_ticket == MaxTicket) return;

        int origin = _ticket;
        if (_TicketHealingTimer > TicketHealTime * MaxTicket)
        {
            PlayerPrefs.SetInt("TicketHealStartTime", RealTimeTick);
            _ticket = MaxTicket;
        }
        else if (_TicketHealingTimer > TicketHealTime)
        {
            int count = Mathf.Min(MaxTicket - _ticket, (int)(_TicketHealingTimer / TicketHealTime));
            _ticket += count;
            PlayerPrefs.SetInt("TicketHealStartTime", PlayerPrefs.GetInt("TicketHealStartTime") + TicketHealTime * count);
        }

        OnChangedTicket?.Invoke(_ticket - origin);
        PlayerPrefs.SetInt("ticket", _ticket);
    }
}
