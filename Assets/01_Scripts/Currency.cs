using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static int Ticket
    {
        get => _ticket;
        set
        {
            int origin = _ticket;
            _ticket = value;
            PlayerPrefs.SetInt("ticket", value);

            OnChangedTicket?.Invoke(value - origin);
        }
    }
}
