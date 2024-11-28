using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionPlayer : Player
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
                return string.Format("죽기 전 사망 유예 효과를\n얻고, 최대 {0}초 안에 젬을 한칸\n채우면 부활합니다.", GetResurrectionBuffDurationTime(MaxLevel));
            }
            else
            {
                return string.Format("죽기 전 사망 유예 효과를\n얻고, {0}초 안에 젬을 한칸\n채우면 부활합니다.", GetResurrectionBuffDurationTime(PlayerLevel));
            }
        }
    }

    public override int MaxHealth => 2;
    public override float MaxBoostGazy => 2;

    [SerializeField] private GameObject _resurrectionEffect;

    private float GetResurrectionBuffDurationTime(int playerLevel)
    {
        return 6 + playerLevel * 0.2f;
    }

    public override void Kill()
    {
        if (transform.position.y < -0.2f || _buffSystem.ContainBuff<ResurrectionBuff>())
        {
            base.Kill();
        }
        else
        {
            _buffSystem.AddBuff(new ResurrectionBuff(GetResurrectionBuffDurationTime(PlayerLevel), _resurrectionEffect));
        }
    }
}