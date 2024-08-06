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
        MagnetBuff magnetBuff = new MagnetBuff(_magnetStrength, _durationTime);
        p.GetComponent<BuffSystem>().AddBuff(magnetBuff);
    }
}
