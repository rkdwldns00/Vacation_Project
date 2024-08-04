using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxGemPlayer : Player
{
    protected override void Awake()
    {
        MaxBoostGazy += PlayerLevel / 5f;
        base.Awake();
    }
}
