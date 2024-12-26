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
                return string.Format("모든 체력 소진 시 사망 유예 효과를 얻고, 최대 {0}초 안에 젬을 한칸 채우면 부활합니다. 부활 중 아이템을 얻지 못합니다.", GetResurrectionBuffDurationTime(MaxLevel));
            }
            else
            {
                return string.Format("모든 체력 소진 시 사망 유예 효과를 얻고, {0}초 안에 젬을 한칸 채우면 부활합니다. 부활 중 아이템을 얻지 못합니다.", GetResurrectionBuffDurationTime(PlayerLevel));
            }
        }
    }
    public override float CarInfoFontSize => 40;

    public override int MaxHealth => 2;
    public override float MaxBoostGazy => 2;

    [SerializeField] private GameObject _resurrectionEffect;

    private float GetResurrectionBuffDurationTime(int playerLevel)
    {
        return 6 + playerLevel * 0.2f;
    }

    public override void Kill()
    {
        if (transform.position.y < -0.2f || BuffSystem.ContainBuff<ResurrectionBuff>())
        {
            base.Kill();
        }
        else
        {
            BuffSystem.AddBuff(new ResurrectionBuff(GetResurrectionBuffDurationTime(PlayerLevel), _resurrectionEffect));
        }
    }
}