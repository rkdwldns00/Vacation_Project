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

    public void RemoveBuff(Buff buff)
    {
        _buffs.Remove(buff);
    }

    private void Update()
    {
        for (int i=0; i< _buffs.Count; i++)
        {
            _buffs[i].UpdateBuff(this);
        }
    }
}
