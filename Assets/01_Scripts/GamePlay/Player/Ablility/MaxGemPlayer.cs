using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxGemPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 5;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("젬 저장량이 최대 {0}개 늘어납니다.", GetAddMax(MaxLevel));
            }
            else
            {
                return string.Format("젬 저장량이 {0}개 늘어납니다.", GetAddMax(PlayerLevel));
            }
        }
    }

    private float GetAddMax(int playerLevel)
    {
        return playerLevel / 5f;
    }

    protected override void Awake()
    {
        MaxBoostGazy += GetAddMax(PlayerLevel);
        base.Awake();
    }
}
