using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPlayer : Player
{
    public override int MaxLevel => 2;
    public override int UpgradeCost => 500;
    public override int UnlockCost => 10;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("더욱 단단해졌습니다,\n체력이 최대 {0}칸 늘어납니다.", MaxLevel);
            }
            else
            {
                return string.Format("더욱 단단해졌습니다,\n체력이 {0}칸 늘어납니다.", PlayerLevel);
            }
        }
    }

    public override int MaxHealth => base.MaxHealth + 1;
}