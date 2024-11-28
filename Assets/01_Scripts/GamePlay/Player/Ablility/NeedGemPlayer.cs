using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedGemPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 100;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("젬 요구량이 최대 {0}% 줄어듭니다.", (int)(100 - GetNeedGem(MaxLevel) * 100));
            }
            else
            {
                return string.Format("젬 요구량이 {0}% 줄어듭니다.", (int)(100 - GetNeedGem(PlayerLevel) * 100));
            }
        }
    }

    float GetNeedGem(int playerLevel)
    {
        return 1 - playerLevel / 100f * 3;
    }

    public override bool UseBoost()
    {
        float needGem = GetNeedGem(PlayerLevel);
        if (BoostGazy > needGem)
        {
            BoostGazy -= needGem;
            RunOnChargeBoostGazy(-needGem);
            return true;
        }
        return false;
    }
}
