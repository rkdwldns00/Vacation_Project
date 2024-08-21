using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => base.UpgradeCost;
    public override int UnlockCost => 15;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("더욱 빨라졌습니다,\n속도가 최대 {0}% 빨라집니다.", (int)(GetSpeedRate(MaxLevel) * 100 - 100));
            }
            else
            {
                return string.Format("더욱 빨라졌습니다,\n속도가 {0}% 빨라집니다.", (int)(GetSpeedRate(PlayerLevel) * 100 - 100));
            }
        }
    }

    public override float MoveSpeed {
        get => base.MoveSpeed * GetSpeedRate(PlayerLevel); 
    }

    private float GetSpeedRate(int playerLevel)
    {
        return 1f + playerLevel / 10f;
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
