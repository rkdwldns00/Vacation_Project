using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 175;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("부스트 아이템의 효과가\n최대 {0}% 증가합니다.", Mathf.Round(GetBoostSpeedRate(MaxLevel) * 100 - 100));
            }
            else
            {
                return string.Format("부스트 아이템의 효과가\n최대 {0}% 증가합니다.", Mathf.Round(GetBoostSpeedRate(PlayerLevel) * 100 - 100));
            }
        }
    }

    private float GetBoostSpeedRate(int playerLevel)
    {
        return 1.5f + playerLevel / 20f;
    }

    protected override void Awake()
    {
        base.Awake();
        BuffSystem.OnAddBuff += OnAddBuff;
    }

    private void OnAddBuff(Buff AddedBuff)
    {
        if (AddedBuff is BoostBuff boostBuff)
        {
            boostBuff.ChangeBoostSpeed(boostBuff.BoostSpeed * GetBoostSpeedRate(PlayerLevel));
        }
    }
}
