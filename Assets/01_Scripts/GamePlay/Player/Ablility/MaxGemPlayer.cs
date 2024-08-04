using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxGemPlayer : Player
{
    protected override void Start()
    {
        MaxBoostGazy += PlayerLevel / 5f;
        base.Start();
    }
}
