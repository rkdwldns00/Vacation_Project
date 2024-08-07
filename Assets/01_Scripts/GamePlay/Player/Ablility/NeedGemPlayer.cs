using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedGemPlayer : Player
{
    public override bool UseBoost()
    {
        float needGem = 1 - PlayerLevel / 20f;
        if (BoostGazy > needGem)
        {
            BoostGazy -= needGem;
            RunOnChargeBoostGazy(-needGem);
            return true;
        }
        return false;
    }
}
