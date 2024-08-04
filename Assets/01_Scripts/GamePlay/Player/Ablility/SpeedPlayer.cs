using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlayer : Player
{
    protected override void Start()
    {
        MoveSpeed *= 1f + PlayerLevel / 10f;
        base.Start();
    }
}
