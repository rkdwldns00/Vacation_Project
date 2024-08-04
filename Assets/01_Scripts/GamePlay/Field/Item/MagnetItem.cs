using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagnetItem : Item
{
    [SerializeField] private float _magnetStrength;
    [SerializeField] private float _durationTime;

    protected override void UseItem(Player p)
    {
        MagnetBuff m = p.GetComponent<MagnetBuff>();
        if (m == null) m = p.AddComponent<MagnetBuff>();

        m.SetMagnetBuffData(_magnetStrength, _durationTime);
    }
}
