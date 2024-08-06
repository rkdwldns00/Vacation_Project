using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    private List<Buff> _buffs = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        _buffs.Add(buff);
        buff.StartBuff(this);
    }

    private void Update()
    {
        foreach (Buff buff in _buffs)
        {
            buff.UpdateBuff(this);
        }
    }
}
