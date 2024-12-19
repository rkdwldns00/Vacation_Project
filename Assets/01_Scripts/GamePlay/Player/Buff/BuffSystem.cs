using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 이기환 */
public class BuffSystem : MonoBehaviour
{
    public event Action<Buff> OnAddBuff;
    public event Action<Buff> OnRemoveBuff;

    public List<Buff> Buffs { get; private set; } = new List<Buff>();

    public Buff AddBuff(Buff buff)
    {
        if (ContainBuff(buff))
        {
            return MergeBuff(buff);
        }
        else
        {
            Buffs.Add(buff);
            buff.StartBuff(this);

            OnAddBuff?.Invoke(buff);

            return buff;
        }
    }

    public Buff MergeBuff(Buff buff)
    {
        foreach (Buff hasBuff in Buffs)
        {
            if (buff.GetType() == hasBuff.GetType())
            {
                hasBuff.MergeBuff(buff);
                return hasBuff;
            }
        }
        return null;
    }

    public void RemoveBuff(Buff buff)
    {
        Buffs.Remove(buff);
        buff.EndBuff(this);

        OnRemoveBuff?.Invoke(buff);
    }

    public bool ContainBuff<T>() where T : Buff
    {
        foreach (Buff buff in Buffs)
        {
            if (buff.GetType() == typeof(T)) return true;
        }

        return false;
    }

    public bool ContainBuff(Buff buff)
    {
        foreach (Buff containBuff in Buffs)
        {
            if (buff.GetType() == containBuff.GetType()) return true;
        }
        
        return false;
    }

    private void Update()
    {
        for (int i=0; i< Buffs.Count; i++)
        {
            Buffs[i].UpdateBuff(this);
        }
    }

    private void OnDestroy()
    {
        while (Buffs.Count > 0) { 
            RemoveBuff( Buffs[0]);
        }
    }
}
