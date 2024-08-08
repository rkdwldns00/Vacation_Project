using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Currency
{
    private static int _gold = PlayerPrefs.GetInt("gold");
    private static int _crystal = PlayerPrefs.GetInt("crystal");
    private static int _ticket = PlayerPrefs.GetInt("ticket");

    public static int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            PlayerPrefs.SetInt("gold", value);
        }
    }

    public static int Crystal
    {
        get => _crystal;
        set
        {
            _crystal = value;
            PlayerPrefs.SetInt("crystal", value);
        }
    }

    public static int Ticket
    {
        get => _ticket;
        set
        {
            _ticket = value;
            PlayerPrefs.SetInt("ticket", value);
        }
    }
}
