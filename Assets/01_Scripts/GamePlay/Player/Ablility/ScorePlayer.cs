using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayer : Player
{
    float Bonus => 1.5f + PlayerLevel / 10;

    public override void ChargeBoost(float value)
    {
        value *= Bonus;
        BoostGazy += value;
        GameManager.Instance.GemScore += 10;
        RunOnChangedBoostGazy(value);
    }

    protected override void UpdaateDistanceScore()
    {
        GameManager.Instance.DistanceScore = (int)(transform.position.z * Bonus);
    }
}