using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPlayer : Player
{
    protected override void Start()
    {
        MaxHealth += 1;
        base.Start();
    }
}
