using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 20;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("골드획득량이 최대 {0}% 증가합니다.", (int)(GetGoldRate(MaxLevel) * 1000 - 100));
            }
            else
            {
                return string.Format("골드획득량이 {0}% 증가합니다.", (int)(GetGoldRate(PlayerLevel) * 1000 - 100));
            }
        }
    }

    private float GetGoldRate(int playerLevel)
    {
        return 0.12f + playerLevel * 0.005f;
    }

    protected override void SetGoldRate()
    {
        GameManager.Instance.RewardGoldRate = GetGoldRate(PlayerLevel);
    }
}
