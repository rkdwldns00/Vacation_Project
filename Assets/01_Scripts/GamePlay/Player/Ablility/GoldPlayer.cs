using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlayer : Player
{
    public override int MaxLevel => 3;
    public override int UpgradeCost => base.UpgradeCost * 3;
    public override int UnlockCost => 200;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("젬을 획득할 때 최대 {0}골드를 획득합니다.", GetGold(MaxLevel));
            }
            else
            {
                return string.Format("젬을 획득할 때 {0}골드를 획득합니다.", GetGold(PlayerLevel));
            }
        }
    }   

    private int GetGold(int playerLevel)
    {
        return playerLevel;
    }

    public override void ChargeBoost(float value)
    {
        base.ChargeBoost(value);

        GameManager.Instance.RewardGoldAdded += PlayerLevel;
    }
}
