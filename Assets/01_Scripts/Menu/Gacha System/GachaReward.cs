using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GachaReward : ScriptableObject
{
    public Sprite sprite;

    public abstract string GetName(int rate);
    public abstract void GetReward(int rate);
}
