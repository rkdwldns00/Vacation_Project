using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 이기환 */
public class BuffSystem : MonoBehaviour
{
    private List<Buff> _buffs = new List<Buff>();

    public Buff AddBuff(Buff buff)
    {
        if (ContainBuff(buff))
        {
            return MergeBuff(buff);
        }
        else
        {
            _buffs.Add(buff);
            buff.StartBuff(this);

            return buff;
        }
    }

    public Buff MergeBuff(Buff buff)
    {
        foreach (Buff hasBuff in _buffs)
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
        _buffs.Remove(buff);
        buff.EndBuff(this);
    }

    public bool ContainBuff<T>() where T : Buff
    {
        foreach (Buff buff in _buffs)
        {
            if (buff.GetType() == typeof(T)) return true;
        }

        return false;
    }

    public bool ContainBuff(Buff buff)
    {
        foreach (Buff containBuff in _buffs)
        {
            if (buff.GetType() == containBuff.GetType()) return true;
        }
        
        return false;
    }

    private void Update()
    {
        for (int i=0; i< _buffs.Count; i++)
        {
            _buffs[i].UpdateBuff(this);
        }
    }

    private void OnDestroy()
    {
        while (_buffs.Count > 0) { 
            RemoveBuff( _buffs[0]);
        }
    }
}
