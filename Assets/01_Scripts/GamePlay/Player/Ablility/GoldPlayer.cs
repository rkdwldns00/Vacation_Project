using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlayer : Player
{
    protected override void SetGoldRate()
    {
        GameManager.Instance.RewardGoldRate = 0.12f + PlayerLevel * 0.005f;
    }
}
