using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GachaReward : ScriptableObject
{
    public Sprite sprite;

    public abstract void GetReward(int rate);
}
