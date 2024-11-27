using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlayer : Player
{
    public override int MaxLevel => 10;
    public override int UpgradeCost => 150;
    public override int UnlockCost => 150;
    public override string CarInfo
    {
        get
        {
            if (PlayerLevel == 0)
            {
                return string.Format("죽기전 무적 효과를 얻고,\n최대 {0}초 안에 젬을 한칸\n채우면 부활합니다.", GetResurrectionBuffDurationTime(MaxLevel));
            }
            else
            {
                return string.Format("죽기전 무적 효과를 얻고,\n{0}초 안에 젬을 한칸\n채우면 부활합니다.", GetResurrectionBuffDurationTime(PlayerLevel));
            }
        }
    }

    public override int MaxHealth => 2;
    public override float MaxBoostGazy => 2;

    private float GetResurrectionBuffDurationTime(int playerLevel)
    {
        return playerLevel * 1.5f;
    }

    protected override void DieHandler()
    {
        if(transform.position.y < -0.2f)
        {
            base.DieHandler();
        }
        _buffSystem.AddBuff(new ResurrectionBuff(GetResurrectionBuffDurationTime(PlayerLevel)));
    }

    public void ResurrectionFail()
    {
        base.DieHandler();
    }
}